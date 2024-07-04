using UnityEngine;
public class Coin : MonoBehaviour
{
    private CanvasController cV;
    private void Start()
    {
        cV = FindAnyObjectByType<CanvasController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            cV._coinsInt = cV._coinsInt + 1;
            PlayerPrefs.SetInt("coinsInt", cV._coinsInt);
            Destroy(other.gameObject);
        }
    }
}
