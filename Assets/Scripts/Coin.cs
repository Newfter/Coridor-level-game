using UnityEngine;
public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        FindAnyObjectByType<AudioController>().Audio(_audioClip, transform.position);
        Destroy(gameObject);
    }
}