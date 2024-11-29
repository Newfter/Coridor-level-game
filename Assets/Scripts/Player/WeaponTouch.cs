using JetBrains.Annotations;
using UnityEngine;

public class WeaponTouch : MonoBehaviour
{
    public bool haveGun; 
    [NotNull] public GameObject bulletPanel, press1ToTakeGun,  pressEToTakeAmmo;
    public Transform instGuns, gunInHand;
    public Gun gun;
    public PlayerBullets pB;
    private void Start()
    {
        pB = FindAnyObjectByType<PlayerBullets>();
        press1ToTakeGun.SetActive(false);
        pressEToTakeAmmo.SetActive(false);
        bulletPanel.SetActive(false);
    }
    
    private void Update()
    {
        bulletPanel.SetActive(false);
        if(haveGun) bulletPanel.SetActive(true);
        Raycast();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
            pressEToTakeAmmo.SetActive(false);
        }
    } 
    
    private void Raycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            var hitTransform = hit.collider.transform;
            if (hitTransform.gameObject.CompareTag("Gun")) PointAtGun(hitTransform);
            else press1ToTakeGun.SetActive(false);
            if (hitTransform.gameObject.CompareTag("Ammo"))
            {
                pressEToTakeAmmo.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hitTransform.gameObject.TryGetComponent(out AmmoController ammoController))
                    {
                        pB.ReceiveAmmo(ammoController.gun);
                    }
                    Destroy(hitTransform.gameObject);
                }
            }
            else{pressEToTakeAmmo.SetActive(false);}
        }

    }

    private void PointAtGun(Transform hitTransform)
    {
        press1ToTakeGun.SetActive(true);
        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;
        gun = hitTransform.gameObject.GetComponent<Gun>();
        gun.EnableGun();
        if(haveGun)
        {
            gunInHand.position = hitTransform.position;
            hitTransform.position = instGuns.position;
                        
            gunInHand.rotation = hitTransform.rotation;
            hitTransform.rotation = instGuns.rotation;
                        
            gunInHand.parent = hitTransform.parent;
            hitTransform.parent = instGuns.parent;
            gunInHand = hitTransform;
            var bulletAmount = pB.ReturnTotalBullets(gun.weaponSO.type);
            pB.TextUpdate(bulletAmount);
        }
        else
        {
            hitTransform.parent = instGuns;
            hitTransform.localPosition = Vector3.zero;
            hitTransform.localRotation = Quaternion.identity;
            haveGun = true;
            gunInHand = hitTransform;
                        
        }
        
        gunInHand.GetComponent<Rigidbody>().isKinematic = true;
    }
    
    private void OnDrawGizmos() { Gizmos.DrawSphere(instGuns.position, 0.1f); }
    
    public void DropWeapon()
    {
        if(gunInHand is null) return;
        var r = gunInHand.GetComponent<Rigidbody>();
        r.isKinematic = false;
        r.AddForce(transform.forward * 200);
        gunInHand.parent = null;
        gunInHand = null;
        haveGun = false;
    }
}