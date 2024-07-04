using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI maxBullets, bulletsLeft;

    public void InitTextBullet(int bulletAmount)
    {
        maxBullets.text = bulletAmount.ToString() + "/";
        bulletsLeft.text = bulletAmount.ToString();
    }

    public void UpdateAmountOfBuller(int bulletAmount)
    {
        bulletsLeft.text = bulletAmount.ToString();
    }
    
    
}
