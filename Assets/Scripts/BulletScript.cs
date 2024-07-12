using UnityEngine;
public class BulletScript : MonoBehaviour
{
    private Damageable damageable;
    public void BulletDamage(GameObject gm,int damage)
    {
        if (gm.TryGetComponent(out damageable)) damageable.TakeDamage(damage);
        else if( gm.TryGetComponent(out LinkToGm LinkToGM))
        {
            if (LinkToGM.GameObject.TryGetComponent(out damageable)) damageable.TakeDamage(damage);
        }
    }
    private void OnCollisionEnter(Collision other) { if(other.gameObject.CompareTag("Zombie")){BulletDamage( other.gameObject, 10);} }
}
