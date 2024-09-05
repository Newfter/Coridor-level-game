using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponTouch : MonoBehaviour
{
    public bool haveGun = false; 
    [SerializeField] private GameObject bulPanel, weaponButton,  pressT;
    public Transform instGuns, gunInHand;
    public Gun gun;
    public PlayerBullets pB;

    private void Start()
    {
        pB = FindAnyObjectByType<PlayerBullets>();
        weaponButton.SetActive(true);
        pressT.SetActive(false);
    }
    
    private void Update()
    {
        bulPanel.SetActive(false);
        if(haveGun) bulPanel.SetActive(true);
        Raycast();
        if (Input.GetKeyDown(KeyCode.Q)) { DropWeapon(); }
    } 
    
    private void Raycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var hitTransform = hit.collider.transform;
            if (hitTransform.gameObject.CompareTag("Gun")) PointAtGun(hitTransform);
            else weaponButton.SetActive(false);
            if (!gunInHand) return;
            if (hitTransform.gameObject.CompareTag("Ammo"))
            {
                pressT.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hitTransform.gameObject.TryGetComponent(out AmmoController ammoController))
                    {
                        pB.ReceiveAmmo(ammoController.gun);
                    }
                    Destroy(hitTransform.gameObject);
                }
            }
            else{pressT.SetActive(false);}
        }

    }

    private void PointAtGun(Transform hitTransform)
    {
        weaponButton.SetActive(true);
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