using System.Collections;
using TMPro;
using UnityEngine;
public class InstGrenata : MonoBehaviour
{
    public GameObject grenadePanel;
    [SerializeField] private GameObject instGrenata;
    [SerializeField] private int force;
    public TextMeshProUGUI totalGrenadeLeft;
    private PlayerBullets pB;
    private bool canThrou = true;

    private void Start()
    {
        pB = FindAnyObjectByType<PlayerBullets>();
        grenadePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)&& canThrou)
        {
            var grenadeAmount = pB.ReturnTotalBullets(TypeGun.Grenade);
            if (grenadeAmount > 0) { StartCoroutine(Throu()); }
        }
    }

    private IEnumerator Throu()
    {
        canThrou = false;
        var grenadeAmount = pB.ReturnTotalBullets(TypeGun.Grenade);
        GameObject gr = Instantiate(instGrenata, transform.position + Vector3.up, Quaternion.identity);
        gr.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * force);
        grenadeAmount -= 1;
        pB.MinusTotalBullets(grenadeAmount, TypeGun.Grenade);
        totalGrenadeLeft.text = grenadeAmount.ToString();
        if(grenadeAmount == 0) grenadePanel.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        canThrou = true;
    }
}
