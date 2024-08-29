using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    public List<WeaponAndClip> weaponClips;

    public void ReceiveAmmo(TypeGun type)
    {
        foreach (var variableWeaponClip in weaponClips)
        {
            if (variableWeaponClip.weaponSO.type == type)
                variableWeaponClip.totalBulletAmount += variableWeaponClip.weaponSO.clipAmount;
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

}

[Serializable]
public class WeaponAndClip
{
    public WeaponSO weaponSO;
    public int totalBulletAmount;
}