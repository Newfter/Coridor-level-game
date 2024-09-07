using UnityEngine;
public class Coin : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.GetInt("coinsInt") == 0) PlayerPrefs.SetInt("coinsInt", 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("coinsInt", PlayerPrefs.GetInt("coinsInt") + 1);
        }
    }
}