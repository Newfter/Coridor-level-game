using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class CanvasController : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private GameObject mode, settings, sOn,sOff,mOn,mOff;
    [SerializeField] private AudioMixer music;
    private ZombieCraetion zC;
    public void Start()
    {
        settings.SetActive(false);
        zC = FindObjectOfType<ZombieCraetion>();
        mode.SetActive(false);
    }
    public void modeOn() { mode.SetActive(true); }
    public void ModeOff(){ mode.SetActive(false);}
    public void Play() { SceneManager.LoadScene("Scenes/SimpleScene"); }
    public void Easy() { PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Easy.ToString()); }
    public void Medium(){PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Medium.ToString());}
    public void Hard() {PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Hard.ToString());}
    public void Settings() { settings.SetActive(true); }
    public void OffSettings() { settings.SetActive(false); }
    public void SoundOn()
    {
        music.SetFloat("Sound", 0);
        sOn.SetActive(false);
        sOff.SetActive(true);
    }
    public void SoundOff()
    {
        music.SetFloat("Sound", -80);
        sOff.SetActive(false);
        sOn.SetActive(true);
    }
    public void MusicOn()
    {
        music.SetFloat("Music", 0);
        mOn.SetActive(false);
        mOff.SetActive(true);
    }
    public void MusicOff()
    {
        music.SetFloat("Music", -80);
        mOff.SetActive(false);
        mOn.SetActive(true);
    }
}