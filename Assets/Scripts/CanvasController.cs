using UnityEngine;
public class CanvasController : MonoBehaviour
{
    public AudioSource click;
    public void Restart()
    {
        click.Play();
        Application.LoadLevel(Application.loadedLevel);
    }
}