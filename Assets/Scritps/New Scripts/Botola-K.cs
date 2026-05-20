using UnityEngine;
using UnityEngine.SceneManagement;

public class Botola : MonoBehaviour
{
    [Header("Chiavi richieste")]
    public Sprite chiave1;
    public Sprite chiave2;
    public Sprite chiave3;

    [Header("Apertura")]
    public float distanzaMax = 3f;
    public float velocitaApertura = 2f;
    public float altezzaApertura = 2f;

    [Header("Scena caverna")]
    public string nomeScena = "Caverna";

    private Inventario inventario;
    private Transform player;
    private bool aperta = false;
    private bool staAprendo = false;
    private Vector3 posizioneIniziale;
    private Vector3 posizioneFinale;

    void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        posizioneIniziale = transform.position;
        posizioneFinale = transform.position - Vector3.up * altezzaApertura;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (staAprendo)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                posizioneFinale,
                Time.deltaTime * velocitaApertura
            );
            return;
        }

        float distanza = Vector3.Distance(transform.position, player.position);
        if (distanza <= distanzaMax)
        {
            Debug.Log("Distanza OK: " + distanza + " | Aperta: " + aperta);
            Debug.Log("Chiave1: " + inventario.HaOggetto(chiave1));
            Debug.Log("Chiave2: " + inventario.HaOggetto(chiave2));
            Debug.Log("Chiave3: " + inventario.HaOggetto(chiave3));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!aperta)
                TentaApertura();
            else
                CaricaCaverna();
        }
    }

    void TentaApertura()
    {
        if (inventario.HaOggetto(chiave1) &&
            inventario.HaOggetto(chiave2) &&
            inventario.HaOggetto(chiave3))
        {
            inventario.RimuoviOggetto(chiave1);
            inventario.RimuoviOggetto(chiave2);
            inventario.RimuoviOggetto(chiave3);
            aperta = true;
            staAprendo = true;
            Debug.Log("Botola aperta!");
        }
        else
        {
            Debug.Log("Hai bisogno di 3 chiavi!");
        }
    }

    void CaricaCaverna()
    {
        SceneManager.LoadScene(nomeScena);
    }
}