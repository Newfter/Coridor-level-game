using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "weapon", menuName = "WeaponSO", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        public string name;
        public GameObject bullet;

    }
}