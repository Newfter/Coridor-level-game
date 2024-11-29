using System;
using System.Collections;
using UnityEngine;
public class Stick : MonoBehaviour
{
    private const int StickBaseDamage = 2;
    [SerializeField] private Animator stickAnimator;
    private Damageable damageable;
    private void Start() { gameObject.GetComponent<MeshRenderer>().enabled = false; }
    private void Update()
    {
        var animInProgress = stickAnimator.GetCurrentAnimatorStateInfo(0).IsName("StickAnim");
        if (Input.GetKeyDown(KeyCode.V) && !animInProgress) { StartCoroutine(StickCorutine()); }
    }

    private IEnumerator StickCorutine()
    {
        if(gameObject.GetComponent<MeshRenderer>().enabled) {yield break;}
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        stickAnimator.SetBool("IsHitting", true);
        yield return new WaitForSeconds(0.1f);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f ,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
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
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
