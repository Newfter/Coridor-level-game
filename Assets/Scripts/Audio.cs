using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource pistolShot, desertEagle, gunReloading, pickingAweapon;
    private WeaponTouch wp;
    private void Start()
    {
        wp = FindObjectOfType<WeaponTouch>();
    }

    private void Update()
    {
        if (wp.haveGun){pickingAweapon.Play();}
    }
}
