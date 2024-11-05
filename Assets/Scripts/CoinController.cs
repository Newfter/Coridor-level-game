using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool[] isSpawned;
    private int pause = 60;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private IEnumerator CoinSpawn()
    {
        while (true)
        {
            
            while (true)
            {
                yield return null;
                
                    break;
            }
            
            
            
            yield return new WaitForSeconds(pause);
        }
        
    }
}
