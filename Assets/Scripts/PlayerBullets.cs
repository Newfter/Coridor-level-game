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
    public void PlusBullets(ref int currentBulletAmount, TypeGun type)
    {
        int plus = ;
        
        
    }
    



[Serializable]
public class WeaponAndClip
{
    public WeaponSO weaponSO;
    public int totalBulletAmount;
}