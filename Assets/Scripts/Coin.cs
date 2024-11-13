using UnityEngine;
public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private CoinController cC;
    private void Start() { cC = FindFirstObjectByType<CoinController>(); }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        cC.coinsAmount += 1;
        PlayerPrefs.SetInt("CoinsAmount", PlayerPrefs.GetInt("CoinsAmount") + 1);
        FindAnyObjectByType<AudioController>().Audio(_audioClip, transform.position);
        Destroy(gameObject);
    }
}