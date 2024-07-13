using System.Collections;
using DefaultNamespace;
using UnityEngine;
public class Gun : MonoBehaviour
{
    public WeaponSO weaponSO;
    [SerializeField] private AudioSource gunShootSource,gunReoadingSource;
    [SerializeField] private Transform bulletThrou;
    private int currentBulletAmount;
    private bool readyToShoot;
    private WeaponTouch wp;
    private CanvasManager currentCanvas;
    private void Start()
    {
        wp = FindAnyObjectByType<WeaponTouch>();
        currentCanvas = FindAnyObjectByType<CanvasManager>();
        currentBulletAmount = weaponSO.bulletAmount;
        readyToShoot = false;
    }
    public void EnableGun()
    {
        readyToShoot = true;
        currentCanvas.InitTextBullet(weaponSO.bulletAmount);
    }
    private void Update() { if (Input.GetKeyDown(KeyCode.Mouse0)&& readyToShoot&& wp.haveGun) {StartCoroutine(Shooting()); } }
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
        gunShootSource.Play();
        readyToShoot = false;
        currentBulletAmount = currentBulletAmount - 1;
        currentCanvas.UpdateAmountOfBuller(currentBulletAmount);
        yield return new WaitForSeconds(weaponSO.speed);
        if (currentBulletAmount <= 0)
        {
            gunReoadingSource.Play();
            yield return new WaitForSeconds(weaponSO.reloadingTime);
            currentBulletAmount = weaponSO.bulletAmount;
        }
        readyToShoot = true;
    }
}
public enum TypeGun
{
    SimplePistol,
    DesertEagle,
    SniperRifle,
    RPG,
    Usi,
    Grenade
}