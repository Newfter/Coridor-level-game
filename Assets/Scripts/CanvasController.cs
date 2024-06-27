using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasController : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private GameObject mode;
    private ZombieCraetion zC;
    public void Start()
    {
        zC = FindObjectOfType<ZombieCraetion>();
        mode.SetActive(false);
    }
    public void modeOn() { mode.SetActive(true); }
    public void ModeOff(){ mode.SetActive(false);}
    public void Play() { SceneManager.LoadScene("Scenes/SimpleScene"); }
    public void Easy() { PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Easy.ToString()); }
    public void Medium(){PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Medium.ToString());}
    public void Hard() {PlayerPrefs.SetString("Mode", ZombieCraetion.Mode.Hard.ToString());}
}