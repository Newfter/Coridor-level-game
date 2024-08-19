using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class WeaponTouch : MonoBehaviour
{
    public bool haveGun = false;
    [SerializeField] private GameObject press1, bulPanel, pressT, toManyBullets;
    public Transform instGuns, gunInHand;
    public Gun gun;

    private void Start() 
    { 
        toManyBullets.SetActive(false);
        press1.SetActive(true);
        pressT.SetActive(false);
    }
    private void Update()
    {
        bulPanel.SetActive(false);
        if(haveGun) bulPanel.SetActive(true);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var hitTransform = hit.collider.transform;
            if (hitTransform.gameObject.CompareTag("Gun"))
            {
                press1.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
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
            }
            else press1.SetActive(false);

            if (!gunInHand) return;
            if (hitTransform.gameObject.CompareTag(gunInHand.gameObject.GetComponent<Gun>().weaponSO.name))
            {
                pressT.SetActive(true);
                if (Input.GetKeyDown(KeyCode.T))
                {
                    if (gun.currentBulletAmount < gun.weaponSO.bulletAmount)
                    {
                        Destroy(hitTransform.gameObject);
                        gun.currentBulletAmount = gun.weaponSO.bulletAmount;
                    }
                    else { StartCoroutine(ToManyBullets()); }
                }
            }
            else{pressT.SetActive(false);}
        }
        if (Input.GetKeyDown(KeyCode.Q)) { DropWeapon(); }
    }
    private void OnDrawGizmos() { Gizmos.DrawSphere(instGuns.position, 0.1f); }

    private IEnumerator ToManyBullets()
    {
        toManyBullets.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        toManyBullets.SetActive(false);
    }
    
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