using System.Collections;
using TMPro;
using UnityEngine;
public class Gun : MonoBehaviour
{
    public TypeGun type;
    [SerializeField] private AudioSource gunShootSource,gunReoadingSource;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletThrou;
    [SerializeField] private int forceOfGun = 100, bulletAmount;
    [SerializeField] private TextMeshProUGUI maxBullets, bulletsLeft;
    private int currentBulletAmount;
    private bool readyToShoot;

    private void Start()
    {
        
        currentBulletAmount = bulletAmount;
        readyToShoot = false;
    }

    public void EnableGun()
    {
        readyToShoot = true;
        maxBullets.text = bulletAmount.ToString() + "/";
        bulletsLeft.text = bulletAmount.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& readyToShoot) {StartCoroutine(Shooting()); }
    }

    private IEnumerator Shooting()
    { 
        GameObject bulletInGame = Instantiate(bullet, bulletThrou.position, bulletThrou.rotation);
        var r = bulletInGame.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(),bulletInGame.GetComponent<Collider>());
        r.isKinematic = false;
        r.AddForce(transform.forward * forceOfGun);
        gunShootSource.Play();
        Destroy(bulletInGame, 3);
        readyToShoot = false;
        currentBulletAmount = currentBulletAmount - 1;
        bulletsLeft.text = currentBulletAmount.ToString();
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