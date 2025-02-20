using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Buttons : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, soundSlider;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private GameObject pausePanel, settingsPanel, taskText, stickText, zombieText, startPanel;
    [SerializeField] private AudioSource click, survive;
    private CoinController cC;

    private void Start()
    {
        StartCoroutine(StartTask());
        pausePanel.SetActive(false);
        cC = FindFirstObjectByType<CoinController>();
        if (PlayerPrefs.GetFloat("Music") != null) { musicSlider.value = PlayerPrefs.GetFloat("Music"); }
        if (PlayerPrefs.GetFloat("Sound") != null) { soundSlider.value = PlayerPrefs.GetFloat("Sound"); } 
        musicMixer.SetFloat("music", musicSlider.value);
        musicMixer.SetFloat("sound", soundSlider.value);
    }

    private IEnumerator StartTask()
    {
        Time.timeScale = 0.1f;
        startPanel.SetActive(true);
        stickText.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Destroy(stickText);
        zombieText.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        survive.Play();
        Destroy(zombieText);
        taskText.SetActive(true);
        Destroy(taskText, 2);
        Destroy(startPanel, 2);
        Destroy(survive, 10);
        Time.timeScale = 1;
    }

    private void Update()
    {
        musicMixer.SetFloat("music", musicSlider.value);
        musicMixer.SetFloat("sound", soundSlider.value);
        if (musicSlider.value != PlayerPrefs.GetFloat("Music")) { PlayerPrefs.SetFloat("Music", musicSlider.value); }
        if (soundSlider.value != PlayerPrefs.GetFloat("Sound")) { PlayerPrefs.SetFloat("Sound", soundSlider.value); }
    }
    public void Menu()
    {
        click.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void TryAgain()
    {
        click.Play();
        SceneManager.LoadScene("Scenes/Game");
    }

    public void Pause()
    {
        click.Play();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        FindAnyObjectByType<FirstPersonController>().enabled = false;
    }
    public void Play()
    {
        click.Play();
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        FindAnyObjectByType<FirstPersonController>().enabled = true;
    }

    public void SetOn()
    {
        click.Play();
        settingsPanel.SetActive(true);
    }

    public void SetOff()
    {
        click.Play();
        settingsPanel.SetActive(false);
    }

    
}
