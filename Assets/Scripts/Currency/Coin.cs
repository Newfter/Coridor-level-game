using UnityEngine;
public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private CoinController cC;
    private void Start() { cC = FindFirstObjectByType<CoinController>(); }


    public void PickupCoin()    
    {
        cC.PlusCoins();
        FindAnyObjectByType<AudioController>().Audio(_audioClip, transform.position);
        Destroy(gameObject);
    }
}