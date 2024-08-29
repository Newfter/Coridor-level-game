using System.Collections;
using DefaultNamespace;
using UnityEngine;
public class Gun : MonoBehaviour
{
    public WeaponSO weaponSO;
    [SerializeField] private Transform bulletThrou;
    [SerializeField] private AudioSource shoot, reload;
    public int currentBulletAmount;
    public bool readyToShoot;
    private WeaponTouch wp;
    private CanvasManager currentCanvas;
    private PlayerBullets pB;
    
    private void Start()
    {
        pB = FindFirstObjectByType<PlayerBullets>();
        wp = FindAnyObjectByType<WeaponTouch>();
        currentCanvas = FindAnyObjectByType<CanvasManager>();
        currentBulletAmount = weaponSO.clipAmount;
        readyToShoot = false;
    }
    public void EnableGun()
    {
        readyToShoot = true;
        currentCanvas.InitTextBullet(weaponSO.clipAmount);
    }

    private void Update()
    {
        if (wp.gun != this){return;}
        if (Input.GetKeyDown(KeyCode.R))
        {
            reload.Play();
            int minus;
            var bulletAmount = pB.ReturnTotalBullets(weaponSO.type);
            if (bulletAmount < weaponSO.clipAmount)
            {
                currentBulletAmount += bulletAmount;
                bulletAmount = 0;
            }
            minus = weaponSO.clipAmount - currentBulletAmount;
            currentBulletAmount = weaponSO.clipAmount;
            bulletAmount -= minus;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&& readyToShoot&& wp.haveGun&& currentBulletAmount >= 0) {StartCoroutine(Shooting()); }
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
        currentCanvas.UpdateAmountOfBuller(currentBulletAmount);
        yield return new WaitForSeconds(weaponSO.speed);
        readyToShoot = currentBulletAmount > 0;
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