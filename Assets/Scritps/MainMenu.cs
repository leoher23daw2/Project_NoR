using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Audio del Menú")]
    public AudioSource musicaMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("V2");
    }

    public void QuitGame()
    {
        if (musicaMenu != null)
        {
            musicaMenu.Stop();
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}