using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private AudioResource[] onRoad, onGrass;
    [SerializeField] private AudioSource walking;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("terrain")) { SetSound(onGrass, walking); }
        else {SetSound(onRoad, walking);}
    }

    public void SetSound(AudioResource[] audioSources, AudioSource aS)
    {
        aS.resource = audioSources[Random.Range(0, audioSources.Length)];
        aS.volume = Random.Range(0.9f, 1.1f);
        aS.pitch = Random.Range(0.9f, 1.1f);
    }
}
