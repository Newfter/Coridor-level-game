using System;
using System.Collections;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel, settingsPanel;
    private CoinController cC;

    private void Start()
    {
        pausePanel.SetActive(false);
        cC = FindFirstObjectByType<CoinController>();
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

    
}
