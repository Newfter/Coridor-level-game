using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private GameObject mode, settings, infoPanel, shopPanel, boostPanel, currencyPanel, skinsPanel;
    [SerializeField] private Slider musicSlider, soundSlider;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private TextMeshProUGUI coins, diamonds, modetext;
    private int _coinsInt, _diamondsInt;
    public void Start()
    {
        if (PlayerPrefs.GetString("Mode") != null) { modetext.text = PlayerPrefs.GetString("Mode"); } 
        _coinsInt = PlayerPrefs.GetInt("coinsInt");
        settings.SetActive(false);
        mode.SetActive(false);
        infoPanel.SetActive(false);
        shopPanel.SetActive(false);
        if (PlayerPrefs.GetFloat("Music") != null) { musicSlider.value = PlayerPrefs.GetFloat("Music"); }
        if (PlayerPrefs.GetFloat("Sound") != null) { soundSlider.value = PlayerPrefs.GetFloat("Sound"); }
    }

    private void Update()
    {
        coins.text = _coinsInt.ToString();
        diamonds.text = _diamondsInt.ToString();
        musicMixer.SetFloat("music", musicSlider.value);
        musicMixer.SetFloat("music", soundSlider.value);
        if (musicSlider.value != PlayerPrefs.GetFloat("Music")) { PlayerPrefs.SetFloat("Music", musicSlider.value); }
        if (soundSlider.value != PlayerPrefs.GetFloat("Sound")) { PlayerPrefs.SetFloat("Sound", musicSlider.value); }
    }

    public void ShopOn()
    {
        shopPanel.SetActive(true);
        click.Play();
    }
    public void ShopOff()
    {
        shopPanel.SetActive(false);
        click.Play();
    }
    public void ShopBoostOn()
    {
        boostPanel.SetActive(true);
        skinsPanel.SetActive(false);
        currencyPanel.SetActive(false);
        click.Play();
    }
    public void ShopSkinsOn()
    {
        boostPanel.SetActive(false);
        skinsPanel.SetActive(true);
        currencyPanel.SetActive(false);
        click.Play();
    }
    public void ShopCurrencyOn()
    {
        boostPanel.SetActive(false);
        skinsPanel.SetActive(false);
        currencyPanel.SetActive(true);
        click.Play();
    }
    public void InfoOn() {infoPanel.SetActive(true); click.Play(); }
    public void InfoOff() {infoPanel.SetActive(false); click.Play();}
    public void modeOn() { mode.SetActive(true); click.Play(); }
    public void ModeOff(){ mode.SetActive(false); click.Play();}

    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/Game");
        click.Play();
    }
    public void Easy() { PlayerPrefs.SetString("Mode", ZombieCreation.Mode.Easy.ToString()); click.Play(); modetext.text = "Easy"; }
    public void Medium(){PlayerPrefs.SetString("Mode", ZombieCreation.Mode.Medium.ToString()); click.Play(); modetext.text = "Medium"; }
    public void Hard() {PlayerPrefs.SetString("Mode", ZombieCreation.Mode.Hard.ToString()); click.Play(); modetext.text = "Hard"; }
    public void SpeedBoost(){}
    public void JumpBoost(){}
    public void AmmoBoost(){}
    public void Settings() { settings.SetActive(true); click.Play();}
    public void OffSettings() { settings.SetActive(false); click.Play();}
}