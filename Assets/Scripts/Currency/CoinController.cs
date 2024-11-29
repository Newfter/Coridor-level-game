using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinController : MonoBehaviour
{
    public TextMeshProUGUI diamondsText, coinText;
    [HideInInspector] public int coinsAmount, diamondsAmount;
    [SerializeField] private GameObject coin, diamond;
    public void PlusDiamonds()
    {
        diamondsAmount += 1;
        diamondsText.text = diamondsAmount.ToString();
        PlayerPrefs.SetInt("DiamondsAmount", PlayerPrefs.GetInt("DiamondsAmount") + 1);
    }
    public void PlusCoins()
    {
        coinsAmount += 1;
        coinText.text = coinsAmount.ToString();
        PlayerPrefs.SetInt("CoinsAmount", PlayerPrefs.GetInt("CoinsAmount") + 1);
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