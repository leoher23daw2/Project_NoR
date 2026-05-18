using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour
{
    [Header("Referencias de la Vela")]
    public Light luzVela;
    public ParticleSystem particulasVela;

    [Header("Configuración Botón Start")]
    public Color colorTransicion = Color.cyan;
    public float intensidadTransicion = 8f;

    [Header("Tiempos")]
    public float tiempoEsperaAntesDeSalir = 1.5f;

    public void OnStartClicked()
    {
        if (luzVela != null)
        {
            luzVela.color = colorTransicion;
            luzVela.intensity = intensidadTransicion;
            SceneManager.LoadScene("Test_Scene");
        }
    }

    public void OnQuitClicked()
    {
        StartCoroutine(ApagarYSalir());
    }

    private IEnumerator ApagarYSalir()
    {
        if (luzVela != null) luzVela.enabled = false;
        if (particulasVela != null) particulasVela.Stop();

        yield return new WaitForSeconds(tiempoEsperaAntesDeSalir);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}