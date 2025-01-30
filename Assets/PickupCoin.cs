using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Coin"))
        {
            hit.gameObject.GetComponent<Coin>().PickupCoin();
        }
    }
}
