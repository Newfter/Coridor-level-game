using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private CoinController cC;
    private void Start() { cC = FindFirstObjectByType<CoinController>(); }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        cC.diamondAmount += 1;
        PlayerPrefs.SetInt("DiamondsAmount", PlayerPrefs.GetInt("DiamondsAmount") + 1);
        cC.diamondText.text = cC.diamondAmount.ToString();
        FindAnyObjectByType<AudioController>().Audio(_audioClip, transform.position);
        Destroy(gameObject);
    }
}