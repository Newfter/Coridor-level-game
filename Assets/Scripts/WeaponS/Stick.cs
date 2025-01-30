using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
public class Stick : MonoBehaviour
{
    private const int StickBaseDamage = 2;
    [SerializeField] private Animator stickAnimator;
    [SerializeField] private AudioResource[] hitAR;
    [SerializeField] private AudioSource stick;
    private Damageable damageable;
    [SerializeField] private GameObject model;
    private WalkingSound wS;

    private void Start()
    {
        wS = FindAnyObjectByType<WalkingSound>();
        model.GetComponent<MeshRenderer>().enabled = false;
    }
    private void Update()
    {
        var animInProgress = stickAnimator.GetCurrentAnimatorStateInfo(0).IsName("StickAnim");
        if (Input.GetKeyDown(KeyCode.V) && !animInProgress) { StartCoroutine(StickCorutine()); }
    }

    private IEnumerator StickCorutine()
    {
        if(model.GetComponent<MeshRenderer>().enabled) {yield break;}

        wS.SetSound(hitAR, stick);
        stick.Play();
        model.GetComponent<MeshRenderer>().enabled = true;
        stickAnimator.SetBool("IsHitting", true);
        yield return new WaitForSeconds(0.1f);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3))
        {
            var hitTransform = hit.collider.transform;
            if (hitTransform.CompareTag("Zombie"))
            {
                if (hitTransform.gameObject.TryGetComponent(out damageable))
                {
                    damageable.TakeDamage(StickBaseDamage);
                }
                
                else if( hitTransform.gameObject.TryGetComponent(out LinkToGm LinkToGM))
                {
                    if (LinkToGM.GameObject.TryGetComponent(out damageable))
                    {
                        damageable.TakeDamage(StickBaseDamage);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.1f);
        stickAnimator.SetBool("IsHitting", false);
        yield return new WaitForSeconds(0.1f);
        model.GetComponent<MeshRenderer>().enabled = false;
    }
}
