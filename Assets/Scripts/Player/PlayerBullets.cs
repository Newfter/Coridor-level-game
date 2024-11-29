using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using TMPro;

public class PlayerBullets : MonoBehaviour
{
    public TextMeshProUGUI totalBullets, bulletsLeft;
    public List<WeaponAndClip> weaponClips;
    private WeaponTouch weaponTouch;
    private InstGrenata iG;

    private void Start()
    {
        iG = FindAnyObjectByType<InstGrenata>();
    }

    public void ReceiveAmmo(TypeGun type)
    {
        foreach (var variableWeaponClip in weaponClips)
        {
            if (variableWeaponClip.weaponSO.type == type)
            {
                variableWeaponClip.totalBulletAmount += variableWeaponClip.weaponSO.clipAmount;
                weaponTouch = FindFirstObjectByType<WeaponTouch>();
                if (type == TypeGun.Grenade)
                {
                    iG.grenadePanel.SetActive(true);
                    iG.totalGrenadeLeft.text = ReturnTotalBullets(TypeGun.Grenade).ToString();
                    return;
                }
                if (weaponTouch.gun == null) return;
                if (weaponTouch.gun.weaponSO.type == type) { totalBullets.text = variableWeaponClip.totalBulletAmount.ToString(); }
            }
        }
    }

    public int ReturnTotalBullets(TypeGun type)
    {
        foreach (var variable in weaponClips)
        {
            if (variable.weaponSO.type == type) return variable.totalBulletAmount;
        }

        return 0;
    }
    public void TextUpdate(int total) { totalBullets.text = total.ToString(); }
    public void MinusTotalBullets(int bulletAmount, TypeGun type)
    {
        foreach (var variable in weaponClips)
        {
            if (variable.weaponSO.type == type)
            {
                variable.totalBulletAmount = bulletAmount;
            }
        }
        
    }

}

[Serializable]
public class WeaponAndClip
{
    public WeaponSO weaponSO;
    public int totalBulletAmount;
}