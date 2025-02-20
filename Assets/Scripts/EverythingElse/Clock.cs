using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;
    private int seconds;
    public bool dead;
    private void Start() { StartCoroutine(ClockVoid()); }
    private IEnumerator ClockVoid()
    {
        yield return new WaitForSeconds(1);
        seconds += 1;
        clockText.text = "" + seconds / 60 + " : " + seconds % 60;
        if(dead) yield break;
        StartCoroutine(ClockVoid());
    }

    public int StopClock()
    {
        dead = true;
        return seconds;
    }
}