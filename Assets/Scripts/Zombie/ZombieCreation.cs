using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieCreation : MonoBehaviour
{
    public Mode mode;
    [SerializeField] private GameObject zombak, lvl2Zombak, lvl3Zombak, lvl4Zombak, zombieBoss, magicZombie;
    [SerializeField] private Transform player, zombieBossSpawn;
    [SerializeField] private Transform[] spawnPoints;
    public TextMeshProUGUI zombiesOnTheMap, zombiesKilledText;
    private CanvasController cC;
    public int pause=25, minus, waitUntilNewZombie = 3, lvl = 1, zombiesOnTheMapInt, zombiesKilled;
    private void Start()
    {
       // mode = Enum.Parse<Mode>(PlayerPrefs.GetString("Mode", Mode.Easy.ToString()));
        minus = mode switch
        {
            Mode.Easy => 1,
            Mode.Medium => 2,
            Mode.Hard => 3,
            _ => throw new ArgumentOutOfRangeException()
        };
        StartCoroutine(IncreasingDificulty());
        StartCoroutine(InstZombak());
        StartCoroutine(BossSpawn());
    }

    private IEnumerator InstZombak()
    {
        while (true)
        {
            Vector3 zombieSpawnPosition = Vector3.zero;
            var minDistance = float.MaxValue;
            foreach (var i in spawnPoints) 
            {
                if (minDistance > Vector3.Distance(i.position, player.position) && Vector3.Distance(i.position, player.position) > 50)
                {
                    minDistance = Vector3.Distance(i.position, player.position);
                    zombieSpawnPosition = i.position;
                }
            }
            

            var x = Random.Range(0, 3);
            for (int i = 0; i <x; i++) 
            {
                switch (lvl)
                {
                    case 1:
                        Instantiate(zombak, zombieSpawnPosition, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(lvl2Zombak, zombieSpawnPosition, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(lvl3Zombak, zombieSpawnPosition, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(lvl4Zombak, zombieSpawnPosition, Quaternion.identity);
                        break;
                }
            }
            yield return new WaitForSeconds(pause);
        }
    }
    public enum Mode
    {
        Easy,
        Medium,
        Hard
    }

    private IEnumerator IncreasingDificulty()
    {
        if (pause - minus > 5) { pause -= minus; }
        waitUntilNewZombie -= 1;
        if (waitUntilNewZombie <= 0 && lvl != 4)
        {
            lvl += 1;
            waitUntilNewZombie = 3;
        }
        yield return new WaitForSeconds(60);
        StartCoroutine(IncreasingDificulty());
    }

    private void PointChoose()
    {
        while (true)
        { 
            zombieBossSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (Vector3.Distance( zombieBossSpawn.position, player.position) > 50)
                break;
        }
    }
    private IEnumerator BossSpawn()
    {
        PointChoose();
        yield return new WaitForSeconds(30);
        Instantiate(magicZombie, zombieBossSpawn.position, Quaternion.identity);
        PointChoose();
        yield return new WaitForSeconds(30);
        Instantiate(magicZombie, zombieBossSpawn.position, Quaternion.identity);
        PointChoose();
        yield return new WaitForSeconds(30);
        Instantiate(magicZombie, zombieBossSpawn.position, Quaternion.identity);
        PointChoose();
        yield return new WaitForSeconds(30);
        Instantiate(zombieBoss,  zombieBossSpawn.position, Quaternion.identity);
        StartCoroutine(BossSpawn());
    }
}