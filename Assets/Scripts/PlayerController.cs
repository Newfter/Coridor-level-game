using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.UI;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ryvok;
    [SerializeField] private Transform sferePos, sferePos2, map2pos;
    [SerializeField] private int speed, jumpForce, ryvokint;
    [SerializeField] private GameObject yep, ryvokgm;
    [SerializeField] private AudioSource audiomen, auwalking;
    [SerializeField] private float radiusFoot ;
    [SerializeField] private float radiusBody ;
    [SerializeField] private int gravity;
    private bool onGround, walking;
    private Rigidbody rb;
    private void Start() {
        ryvokgm.SetActive(false);
        yep.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
            walking = false;
            Collider[] iColl = Physics.OverlapSphere(sferePos.position, radiusFoot);
            for (int i = 0; i < iColl.Length; i++)
            {
                if (iColl[i].CompareTag("Ground")) onGround = true;
            }
            Collider[] pelmColl = Physics.OverlapSphere(sferePos2.position, radiusBody);
            for (int i = 0; i < pelmColl.Length; i++)
            {
                if (pelmColl[i].CompareTag("Ground")|| pelmColl[i].CompareTag("Untagged")) 
                    rb.linearVelocity = transform.TransformDirection( new Vector3(0,rb.linearVelocity.y-gravity, 0));
            }
            if (gameObject.transform.position.y <= -10)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (Input.GetKeyDown(KeyCode.Space)&& onGround) {
                rb.AddForce(Vector3.up * jumpForce);
                onGround = false; }
            if(!onGround)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rb.linearVelocity = transform.TransformDirection( new Vector3(0,rb.linearVelocity.y-gravity, speed));
                    walking = true;
                    
                }
                
                if (Input.GetKey(KeyCode.S)) 
                {
                    rb.linearVelocity = transform.TransformDirection(new Vector3(0,rb.linearVelocity.y-gravity, -speed));
                    walking = true; 
                }
                
                if (Input.GetKey(KeyCode.D))
                {
                    rb.linearVelocity =transform.TransformDirection( new Vector3(speed,rb.linearVelocity.y-gravity, 0));
                    walking = true;
                    
                }
                
                if (Input.GetKey(KeyCode.A)) 
                {
                    rb.linearVelocity =transform.TransformDirection( new Vector3(-speed,rb.linearVelocity.y-gravity, 0));
                    walking = true; 
                }
                
            }
            else 
            {
                if (Input.GetKey(KeyCode.W)) 
                {
                    rb.linearVelocity = transform.TransformDirection( new Vector3(0,rb.linearVelocity.y, speed));
                    walking = true;
                    
                }
                
                if (Input.GetKey(KeyCode.S)) 
                {
                    rb.linearVelocity = transform.TransformDirection(new Vector3(0,rb.linearVelocity.y, -speed));
                    walking = true; 
                }
                
                if (Input.GetKey(KeyCode.D)) 
                {
                    rb.linearVelocity =transform.TransformDirection( new Vector3(speed,rb.linearVelocity.y, 0));
                    walking = true; 
                }
                
                if (Input.GetKey(KeyCode.A)) 
                {
                    rb.linearVelocity =transform.TransformDirection( new Vector3(-speed,rb.linearVelocity.y, 0));
                    walking = true; 
                }
                
            }
            
            
            if (walking) auwalking.volume = 1;
            else auwalking.volume = auwalking.volume - Time.deltaTime;
        }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Finish")) {
            yep.SetActive(true);
            audiomen.Play();
            Destroy(yep ,1f);
            gameObject.transform.position = map2pos.position; }
        if (!other.gameObject.CompareTag("SelfDestroyer")) return;
        onGround = true;
        Destroy(other.gameObject, 1.5f);
    }
    private void OnDrawGizmos() { Gizmos.DrawSphere(sferePos.position, radiusFoot); Gizmos.DrawSphere(sferePos2.position, radiusBody); }
}
