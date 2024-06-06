using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Gun : MonoBehaviour
{
    public TypeGun type;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletThrou;
    [SerializeField] private int forceOfGun = 100, bulletsLeftInt, bulletsLeftIntsimple;
    [SerializeField] private AudioSource pistolShot, gunReloading;
    [SerializeField] private TextMeshProUGUI bulletsLeft;
    private bool readyToShoot;
    private WeaponTouch wp;

    private void Start()
    {
        wp = FindObjectOfType<WeaponTouch>();
        this.enabled = false;
        readyToShoot = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& readyToShoot) {StartCoroutine(Shooting()); }
    }

    private IEnumerator Shooting()
    {
        GameObject bulletInGame = Instantiate(bullet, bulletThrou.position, bulletThrou.rotation);
        bulletInGame.GetComponent<Rigidbody>().AddForce(transform.forward * forceOfGun);
        pistolShot.Play();
        Destroy(bulletInGame, 3);
        readyToShoot = false;
        bulletsLeftInt = bulletsLeftInt - 1;
        bulletsLeft.text = bulletsLeftInt.ToString();
        yield return new WaitForSeconds(0.7f);
        readyToShoot = true;
        if (bulletsLeftInt == 0)
        {
            gunReloading.Play();
            bulletsLeftInt = bulletsLeftIntsimple;
            yield return new WaitForSeconds(2);
        }
    }
}

public enum TypeGun
{
    SimplePistol,
    DesertEagle,
    SniperRifle
}