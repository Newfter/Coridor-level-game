using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinController : MonoBehaviour
{
    [HideInInspector] public int coinsAmount, diamondAmount;
    [SerializeField] private GameObject coin, diamond;
    private void Start()
    {
        coinsAmount = PlayerPrefs.GetInt("CoinsAmount");
    }

    public void CoinSpawn(Vector3 spawn)
    {
        var random = Random.Range(0, 100);
        print(random);
        if (random < 40)
            Instantiate(coin, spawn, Quaternion.identity);
        else if (random < 45) 
            Instantiate(diamond, spawn, Quaternion.identity);
    }
}