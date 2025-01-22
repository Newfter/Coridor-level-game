using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, soundSlider;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private GameObject pausePanel, settingsPanel;
    private CoinController cC;

    private void Start()
    {
        pausePanel.SetActive(false);
        cC = FindFirstObjectByType<CoinController>();
        if (PlayerPrefs.GetFloat("Music") != null) { musicSlider.value = PlayerPrefs.GetFloat("Music"); }
        if (PlayerPrefs.GetFloat("Sound") != null) { soundSlider.value = PlayerPrefs.GetFloat("Sound"); } 
    }

    private void Update()
    {
        musicMixer.SetFloat("music", musicSlider.value);
        musicMixer.SetFloat("music", soundSlider.value);
        if (musicSlider.value != PlayerPrefs.GetFloat("Music")) { PlayerPrefs.SetFloat("Music", musicSlider.value); }
        if (soundSlider.value != PlayerPrefs.GetFloat("Sound")) { PlayerPrefs.SetFloat("Sound", musicSlider.value); }
    }
    

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/Menu");
    }
    public void TryAgain(){SceneManager.LoadScene("Scenes/SimpleScene");}

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        FindAnyObjectByType<FirstPersonController>().enabled = false;
    }
    public void Play()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        FindAnyObjectByType<FirstPersonController>().enabled = true;
    }
    public void SetOn(){settingsPanel.SetActive(true);}
    public void SetOff(){settingsPanel.SetActive(false);}

    
}
