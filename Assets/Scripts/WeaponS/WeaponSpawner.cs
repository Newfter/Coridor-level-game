using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponSpawner : MonoBehaviour
{
    public Transform[] general, rare;
    public GameObject[] wepons;
    public GameObject healingBottle, coin, diamond;
    public float sizeOfGizmos;

    private void Start()
    {
        foreach (Transform i in general) {
            var point = i.gameObject.AddComponent<Point>();
            point.isGeneral = true; }
        foreach (Transform i in rare) {i.gameObject.AddComponent<Point>(); }

        StartCoroutine(ChanceVoid());
    }
    private void Spawn(GameObject gmToSpawn, Transform[] list)
    {
        var randomIndex = Random.Range(0, list.Length);
        Vector3 spawnPosition = list[randomIndex].position;
        if (list[randomIndex].gameObject.GetComponent<Point>().isTaken == false)
        {
            Instantiate(gmToSpawn, spawnPosition, Quaternion.identity);
            list[randomIndex].gameObject.GetComponent<Point>().isTaken = true;
        }
    }

    private IEnumerator ChanceVoid()
    {
        Transform[] list;
        var rarity = Random.Range(0, 10);
        list = rarity > 4 ? general : rare;
        
        int chance = Random.Range(0, 100);
        if(chance is > 75 and < 90) { Spawn(coin, list );}
        if(chance > 90){ Spawn(diamond, list);}
        if(chance is > 40 and < 75) { Spawn(healingBottle, list); }
        else
        {
            var randomWeaponIndex = Random.Range(0, wepons.Length);
            var randomWeapon = wepons[randomWeaponIndex].gameObject;
            Spawn(randomWeapon,list);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ChanceVoid());

    }

    private void OnDrawGizmos()
    {
        foreach (var i in general) { Gizmos.DrawSphere(i.position, sizeOfGizmos); }
        foreach (var i in rare) { Gizmos.DrawSphere(i.position, sizeOfGizmos); }
    }
}
