using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
public class Gun : MonoBehaviour
{
    public TypeGun type;
    public WeaponSO weaponSO;
    [SerializeField] private AudioSource gunShootSource,gunReoadingSource;
    [SerializeField] private Transform bulletThrou;
    [SerializeField] private int forceOfGun = 100, bulletAmount;
    private int currentBulletAmount;
    private bool readyToShoot;
    private WeaponTouch wp;
    private CanvasManager currentCanvas;

    private void Start()
    {
        wp = FindAnyObjectByType<WeaponTouch>();
        currentCanvas = FindAnyObjectByType<CanvasManager>();
        currentBulletAmount = bulletAmount;
        readyToShoot = false;
    }

    public void EnableGun()
    {
        readyToShoot = true;
        currentCanvas.InitTextBullet(bulletAmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& readyToShoot&& wp.haveGun) {StartCoroutine(Shooting()); }
    }

    private IEnumerator Shooting()
    { 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        Vector3 angle;
        if (Physics.Raycast(ray, out hit, 1000))
            angle = (hit.point - bulletThrou.position).normalized;
        else
        {
            angle = transform.forward;
        }    
        GameObject bulletInGame = Instantiate(weaponSO.bullet, bulletThrou.position, bulletThrou.rotation);
        var r = bulletInGame.GetComponent<Rigidbody>();
        r.isKinematic = false;
        r.AddForce(angle * forceOfGun);
        gunShootSource.Play();
        Destroy(bulletInGame, 3);
        readyToShoot = false;
        currentBulletAmount = currentBulletAmount - 1;
        currentCanvas.UpdateAmountOfBuller(currentBulletAmount);
        yield return new WaitForSeconds(0.7f);
      
        if (currentBulletAmount <= 0)
        {
            gunReoadingSource.Play();
            yield return new WaitForSeconds(2);
            currentBulletAmount = bulletAmount;
        }
        readyToShoot = true;
    }
}

public enum TypeGun
{
    SimplePistol,
    DesertEagle,
    SniperRifle,
    RPG
}