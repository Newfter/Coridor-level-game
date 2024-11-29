using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject _damagePanel, _deadPanel;
    public int hp;
    private void Start() { StartCoroutine(Healing()); }
    private IEnumerator Healing()
    {
        if (hp < 10) hp += 1;
        yield return new WaitForSeconds(60);
        StartCoroutine(Healing());
    }

    public void Damage(int damageAmount) { StartCoroutine(PlayerDamage(damageAmount));}
    private IEnumerator PlayerDamage(int damageAmount)
    {
        hp -= damageAmount;
        _damagePanel.SetActive(true);
        if (hp <= 0)
        {
            _deadPanel.SetActive(true);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.2f);
        _damagePanel.SetActive(false);
        
    }
}
