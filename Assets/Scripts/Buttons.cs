using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel, shopPanel, notAnoughMoneyText;
    private CoinController cC;

    private void Start()
    {
        cC = FindFirstObjectByType<CoinController>();
    }

    public void Menu() { SceneManager.LoadScene("Scenes/Menu"); }
    public void TryAgain(){SceneManager.LoadScene("Scenes/SimpleScene");}

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Shop()
    {
        pausePanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void ExitShop()
    {
        shopPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void Play()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    
}
