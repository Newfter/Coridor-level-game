 using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class BrokenLight : MonoBehaviour
{
    private Light component;

    private void Start()
    {
        component = gameObject.GetComponent<Light>();
        StartCoroutine(BrokenLamp());
    }
    private IEnumerator BrokenLamp()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        component.enabled = false;
        yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        component.enabled = true;
        StartCoroutine(BrokenLamp()); 
    }
}
