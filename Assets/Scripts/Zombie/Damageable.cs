using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Damageable : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageText;
    public int hp, zombiesKilled;
    public List<Rigidbody> rigidList;
    private Slider _zombieHp;
    private ZombieCreation _zC;
    private Transform _player;
    private CoinController _coinController;
    private bool _isKilled;

    private void Start()
    {
        _coinController = FindFirstObjectByType<CoinController>();
        _player = FindAnyObjectByType<WeaponTouch>().gameObject.transform;
        _zC = FindAnyObjectByType<ZombieCreation>();
        _zombieHp = GetComponentInChildren<Slider>();
        _zombieHp.maxValue = hp;
        _zombieHp.value = hp;
        damageText.gameObject.SetActive(false);
        //Quaternion.Euler()  from vec3 to quat
        //transform.rotation.euler from quat to vec3
    }
    public void TakeDamage(int damage)
    {
        if (_isKilled) { return; }
        hp -= damage;
        _zombieHp.value = hp;
        StartCoroutine(DamageText(damage));
        if (hp <= 0)
        {
            _isKilled = true;
            _coinController.CoinSpawn(gameObject.transform.position);
            _zC.zombiesKilled += 1;
            _zC.zombiesKilledText.text = _zC.zombiesKilled.ToString();
            _zombieHp.gameObject.SetActive(false);
            zombiesKilled += 1;
            var wtp = GetComponent<WalkingToPlayer>();
            wtp.agent.enabled = false;
            wtp.anim.enabled = false;
            wtp.enabled = false;
            foreach (var i in rigidList) {
                i.isKinematic = false;
                i.useGravity = true;
                i.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }

            Destroy(gameObject, 100);
        }
    }

    private IEnumerator DamageText(int damage)
    {
        
        damageText.gameObject.SetActive(true);
        damageText.transform.LookAt(_player);
        var rot = damageText.transform.rotation.eulerAngles;
        rot.y = 180;
        
        damageText.transform.rotation = Quaternion.Euler(rot);
        
        damageText.text = damage.ToString();
        
        
        yield return new WaitForSeconds(0.5f);
        damageText.gameObject.SetActive(false);
    }
}