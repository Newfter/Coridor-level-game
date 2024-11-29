using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private CoinController cC;
    private void Start() { cC = FindFirstObjectByType<CoinController>(); }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        cC.PlusDiamonds();
        FindAnyObjectByType<AudioController>().Audio(_audioClip, transform.position);
        Destroy(gameObject);
    }
}