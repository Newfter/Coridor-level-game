using UnityEngine;
using UnityEngine.SceneManagement;

public class JustMenu : MonoBehaviour
{
    public void Menu() { SceneManager.LoadScene("Scenes/Menu"); }
    public void TryAgain(){SceneManager.LoadScene("Scenes/SimpleScene");}
}
