using UnityEngine;
public class AudioController : MonoBehaviour
{
    [SerializeField] private GameObject AUDIOsOURCEpREfab;

    public void Audio(AudioClip aC, Vector3 position)
    {
        var GM = Instantiate(AUDIOsOURCEpREfab, position, Quaternion.identity);
        var audioSourceInGame = GM.GetComponent<AudioSource>();
        audioSourceInGame.clip = aC;
        audioSourceInGame.Play();
        float time = audioSourceInGame.clip.length;
        Destroy(GM,  time);
    }
}
