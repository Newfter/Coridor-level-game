using System.Collections;
using TMPro;
using UnityEngine;
public class InstGrenata : MonoBehaviour
{
    public bool haveGrenade;
    public GameObject grenadePanel;
    [SerializeField] private GameObject instGrenata;
    [SerializeField] private int force;
    [SerializeField] private Transform spawnTransform;
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
        var grenadeAmount = pB.ReturnTotalBullets(TypeGun.Grenade);
        if (grenadeAmount > 0) { haveGrenade = true; }
        if (Input.GetKeyDown(KeyCode.G)&& canThrou) { if (grenadeAmount > 0) { StartCoroutine(Throu()); } }
    }

    private IEnumerator Throu()
    {
        canThrou = false;
        var grenadeAmount = pB.ReturnTotalBullets(TypeGun.Grenade);
        GameObject gr = Instantiate(instGrenata, spawnTransform.position, Quaternion.identity);
        gr.GetComponent<Rigidbody>().AddForce((spawnTransform.forward) * force);
        grenadeAmount -= 1;
        pB.MinusTotalBullets(grenadeAmount, TypeGun.Grenade);
        totalGrenadeLeft.text = grenadeAmount.ToString();
        if(grenadeAmount == 0) grenadePanel.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        canThrou = true;
    }
}
