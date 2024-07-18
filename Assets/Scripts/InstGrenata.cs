using UnityEngine;
public class InstGrenata : MonoBehaviour
{
    [SerializeField] private GameObject instGrenata;
    [SerializeField] private int force;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject gr = Instantiate(instGrenata, transform.position + Vector3.up, Quaternion.identity);
            gr.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * force);
        }
    }
}
