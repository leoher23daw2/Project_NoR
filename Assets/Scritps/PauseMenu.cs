using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    private bool inPausa = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inPausa) Pausa();
            else Riprendi();
        }
    }

    void Pausa()
    {
        inPausa = true;
        container.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeButton()
    {
        Riprendi();
    }

    void Riprendi()
    {
        inPausa = false;
        container.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main menu");
    }
}