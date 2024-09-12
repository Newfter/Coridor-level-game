using System.Collections;
using DefaultNamespace;
using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    public WeaponSO weaponSO;
    [SerializeField] private Transform bulletThrou;
    [SerializeField] private AudioSource shoot, reload;
    public int currentBulletAmount;
    public bool readyToShoot;
    private WeaponTouch wp;
    private PlayerBullets pB;
    private bool isReloading = false;
    
    private void Start()
    {
        pB = FindFirstObjectByType<PlayerBullets>();
        wp = FindAnyObjectByType<WeaponTouch>();
        currentBulletAmount = weaponSO.clipAmount;
        readyToShoot = false;
    }

    public void EnableGun()
    {
        readyToShoot = true;
        pB.bulletsLeft.text = currentBulletAmount.ToString();
    }
    private void Update()
    {
        if (wp.gun != this){return;}
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(WaitingAfterReload());
        }
        if (Input.GetKey(KeyCode.Mouse0)&& readyToShoot&& wp.haveGun&& currentBulletAmount > 0&& isReloading == false) {StartCoroutine(Shooting()); }
    }
    private IEnumerator WaitingAfterReload() 
    {
        var bulletAmount = pB.ReturnTotalBullets(weaponSO.type);
        if(isReloading||bulletAmount <= 0)yield break;
        reload.Play();
        int minus;
        if (bulletAmount < weaponSO.clipAmount)
        {
            currentBulletAmount += bulletAmount;
            bulletAmount = 0;
        }
        minus = weaponSO.clipAmount - currentBulletAmount;
        currentBulletAmount = weaponSO.clipAmount;
        bulletAmount -= minus;
        pB.MinusTotalBullets(bulletAmount, weaponSO.type);
        isReloading = true;
        yield return new WaitForSeconds(2);
        isReloading = false;
        pB.bulletsLeft.text = currentBulletAmount.ToString();
        pB.totalBullets.text = bulletAmount.ToString();
        readyToShoot = true;
    }
    private IEnumerator Shooting()
    { 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        Vector3 angle;
        if (Physics.Raycast(ray, out hit, 1000)) {angle = (hit.point - bulletThrou.position).normalized;}
        else { angle = transform.forward; }    
        GameObject bulletInGame = Instantiate(weaponSO.bullet, bulletThrou.position, bulletThrou.rotation);
        var r = bulletInGame.GetComponent<Rigidbody>();
        if (bulletInGame.TryGetComponent(out BulletScript bulletScript)) { bulletScript.weaponSo = weaponSO; }
        if (bulletInGame.TryGetComponent(out RcketScript rcketScript)) { rcketScript.wS = weaponSO;}
        r.isKinematic = false;
        r.AddForce(angle * weaponSO.forceOfGun);
        shoot.Play();
        readyToShoot = false;
        currentBulletAmount = currentBulletAmount - 1;
        
        yield return new WaitForSeconds(weaponSO.speed);
        readyToShoot = currentBulletAmount > 0;
        pB.bulletsLeft.text = currentBulletAmount.ToString();
    }
}
public enum TypeGun
{
    SimplePistol,
    DesertEagle,
    SniperRifle,
    RPG,
    Usi,
    Grenade,
    Ak47
}