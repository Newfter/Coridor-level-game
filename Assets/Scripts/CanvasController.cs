using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class CanvasController : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private GameObject mode, settings, sOn,sOff,mOn,mOff, coinPanel;
    [SerializeField] private AudioMixer music;
    [SerializeField] private TextMeshProUGUI coins;
    public int _coinsInt;
    public void Start()
    {
        _coinsInt = PlayerPrefs.GetInt("coinsInt");
        settings.SetActive(false);
        mode.SetActive(false);
    }
    private void Update() { coins.text = _coinsInt.ToString(); }
    public void modeOn() { mode.SetActive(true); click.Play(); }
    public void ModeOff(){ mode.SetActive(false); click.Play();}
    public void Play() { SceneManager.LoadScene("Scenes/SimpleScene"); click.Play();}
    public void Easy() { PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Easy.ToString()); click.Play();}
    public void Medium(){PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Medium.ToString()); click.Play();}
    public void Hard() {PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Hard.ToString()); click.Play();}
    public void Settings() { settings.SetActive(true); click.Play();}
    public void OffSettings() { settings.SetActive(false); click.Play();}
    
    public void OnCoin() { coinPanel.SetActive(true); click.Play();}
    
    public void OffCoin() {  coinPanel.SetActive(false); click.Play();}
    public void SoundOn()
    {
        music.SetFloat("Sound", 0);
        sOn.SetActive(false);
        sOff.SetActive(true);
        click.Play();
    }
    public void SoundOff()
    {
        music.SetFloat("Sound", -80);
        sOff.SetActive(false);
        sOn.SetActive(true);
        click.Play();
    }
    public void MusicOn()
    {
        music.SetFloat("Music", 0);
        mOn.SetActive(false);
        mOff.SetActive(true);
        click.Play();
    }
    public void MusicOff()
    {
        music.SetFloat("Music", -80);
        mOff.SetActive(false);
        mOn.SetActive(true);
        click.Play();
    }
}