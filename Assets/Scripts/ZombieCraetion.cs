using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieCraetion : MonoBehaviour
{
    public Mode mode;
    [SerializeField] private GameObject zombak;
    [SerializeField] private Transform[] spawnPoints;
    private int maxZombiesPosible;
    public int zombieAmount = 0;
    private WaitForSeconds pause;
    private CanvasController cC;
    private void Start()
    {
        mode = Enum.Parse<Mode>(PlayerPrefs.GetString("Mode", Mode.Easy.ToString()));
        switch (mode)
        {
            case Mode.Easy:
                pause = new WaitForSeconds(20);
                maxZombiesPosible = 25;
                break;
            case Mode.Medium:
                pause = new WaitForSeconds(10);
                maxZombiesPosible = 75;
                break;
            case Mode.Hard:
                pause = new WaitForSeconds(5);
                maxZombiesPosible = 300;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        StartCoroutine(InstZombak());
    }
    private IEnumerator InstZombak()
    {
        if (zombieAmount < maxZombiesPosible)
        {
            zombieAmount = zombieAmount + 1;
            Instantiate(zombak, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
        yield return pause;
        StartCoroutine(InstZombak());
    }
    public enum Mode
    {
        Easy,
        Medium,
        Hard
    }
}
