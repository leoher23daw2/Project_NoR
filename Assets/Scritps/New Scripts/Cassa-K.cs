using UnityEngine;

public class Cassa : MonoBehaviour
{
    [Header("Riferimenti")]
    public Sprite iconaChiave;
    public Transform coperchio;

    [Header("Animazione")]
    public float velocitaApertura = 2f;
    public Vector3 rotazioneAperta = new Vector3(-110f, 0f, 0f);

    private Inventario inventario;
    private bool aperta = false;
    private bool staAprendo = false;
    private Quaternion rotazioneIniziale;
    private Quaternion rotazioneFinale;

    void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        rotazioneIniziale = coperchio.localRotation;
        rotazioneFinale = Quaternion.Euler(rotazioneAperta);
    }

    void Update()
    {
        if (staAprendo)
        {
            coperchio.localRotation = Quaternion.Lerp(
                coperchio.localRotation,
                rotazioneFinale,
                Time.deltaTime * velocitaApertura
            );
            return;
        }

        if (aperta) return;

        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            Debug.Log("Colpito: " + hit.collider.gameObject.name);

            if (hit.collider.transform.IsChildOf(transform) ||
                hit.collider.gameObject == gameObject)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    UsaChiave();
            }
        }
    }

    void UsaChiave()
    {
        if (inventario.HaOggetto(iconaChiave))
        {
            inventario.RimuoviOggetto(iconaChiave);
            ApreCassa();
        }
        else
        {
            Debug.Log("Hai bisogno di una chiave!");
        }
    }

    void ApreCassa()
    {
        aperta = true;
        staAprendo = true;
    }
}