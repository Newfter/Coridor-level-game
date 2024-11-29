using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;
    private int seconds, minutes, time;
    private void Start() { StartCoroutine(ClockVoid()); }
    private IEnumerator ClockVoid()
    {
        yield return new WaitForSeconds(1);
        seconds += 1;
        clockText.text = "" + seconds / 60 + " : " + seconds % 60;
        StartCoroutine(ClockVoid());
    }
}