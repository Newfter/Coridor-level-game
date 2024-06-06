using UnityEngine;
public class WeaponTouch : MonoBehaviour
{
    public bool haveGun = false;
    public GameObject bulLeftText;
    [SerializeField] private GameObject pressI;
    [SerializeField] private Transform instGuns, gunInHand;
    private Gun gun;

    private void Start() { pressI.SetActive(true); }
    private void Update()
    {
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            var hitTransform = hit.collider.transform;
            if (hitTransform.gameObject.CompareTag("Gun"))
            {
                pressI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    
                    var g = hitTransform.GetComponent<Gun>();
                    if(haveGun)
                    {

                        gunInHand.GetComponent<Gun>().enabled = false;
                        gunInHand.position = hitTransform.position;
                        hitTransform.position = instGuns.position;
                        
                        gunInHand.rotation = hitTransform.rotation;
                        hitTransform.rotation = instGuns.rotation;
                        
                        gunInHand.parent = hitTransform.parent;
                        hitTransform.parent = instGuns.parent;
                        gunInHand = hitTransform;
                        g.enabled = true;

                    }
                    else
                    {
                        g.enabled = true;
                        hitTransform.parent = instGuns;
                        hitTransform.localPosition = Vector3.zero;
                        hitTransform.localRotation = Quaternion.identity;
                        haveGun = true;
                        gunInHand = hitTransform;
                    }

                    gunInHand.GetComponent<Rigidbody>().isKinematic = true;
                } 
            }
            else pressI.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Q)){DropWeapon();}
    }
    private void OnDrawGizmos() { Gizmos.DrawSphere(instGuns.position, 0.1f); }
    
    public void DropWeapon()
    {
        var r = gunInHand.GetComponent<Rigidbody>();
        r.isKinematic = false;
        r.AddForce(transform.forward * 200);
        gunInHand.parent = null;
    }
}