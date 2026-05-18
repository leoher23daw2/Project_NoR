using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [Header("Ordine corretto")]
    private string[] codiceCorretto = { "Più", "Diviso", "Meno", "Per" };
    private string[] sequenzaGiocatore = new string[4];
    private int indice = 0;

    [Header("Telecamera puzzle")]
    public Transform cameraPuzzle;
    public Transform cameraOriginale;
    private Camera cam;
    private bool inPuzzle = false;

    [Header("Riferimenti")]
    public GameObject corda;
    public GameObject manos;
    public MonoBehaviour playerMovement;
    public MonoBehaviour mouseLook;

    [Header("Materiali simboli")]
    public Renderer[] simboloPiu;
    public Renderer[] simboloMeno;
    public Renderer[] simboloPer;
    public Renderer[] simboloDiviso;
    public Material materialeNormale;
    public Material materialePremuto;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!inPuzzle) EntraPuzzle();
            else EsciPuzzle();
        }

        if (inPuzzle && Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraPuzzle.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                string tag = hit.collider.tag;
                if (tag == "Simbolo_Più" || tag == "Simbolo_Meno" ||
                    tag == "Simbolo_Per" || tag == "Simbolo_Diviso")
                {
                    string simbolo = tag.Replace("Simbolo_", "");
                    IlluminaSimboло(simbolo);
                    RegistraSimboло(simbolo);
                }
            }
        }
    }

    void IlluminaSimboло(string simbolo)
    {
        Renderer[] renderers = null;
        switch (simbolo)
        {
            case "Più": renderers = simboloPiu; break;
            case "Meno": renderers = simboloMeno; break;
            case "Per": renderers = simboloPer; break;
            case "Diviso": renderers = simboloDiviso; break;
        }
        if (renderers != null)
            foreach (Renderer r in renderers)
                r.material = materialePremuto;
    }

    void RipristinaSimboli()
    {
        foreach (Renderer r in simboloPiu) r.material = materialeNormale;
        foreach (Renderer r in simboloMeno) r.material = materialeNormale;
        foreach (Renderer r in simboloPer) r.material = materialeNormale;
        foreach (Renderer r in simboloDiviso) r.material = materialeNormale;
    }

    void RegistraSimboло(string simbolo)
    {
        if (indice >= 4) return;
        sequenzaGiocatore[indice] = simbolo;
        indice++;
        if (indice == 4) ControllaSequenza();
    }

    void ControllaSequenza()
    {
        bool corretto = true;
        for (int i = 0; i < 4; i++)
        {
            if (sequenzaGiocatore[i] != codiceCorretto[i])
            {
                corretto = false;
                break;
            }
        }

        if (corretto)
        {
            if (corda != null) corda.SetActive(false);
            EsciPuzzle();
        }
        else
        {
            indice = 0;
            sequenzaGiocatore = new string[4];
            RipristinaSimboli();
        }
    }

    void EntraPuzzle()
    {
        inPuzzle = true;
        cameraPuzzle.GetComponent<Camera>().enabled = true;
        cam.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (manos) manos.SetActive(false);
        if (playerMovement) playerMovement.enabled = false;
        if (mouseLook) mouseLook.enabled = false;
    }

    void EsciPuzzle()
    {
        inPuzzle = false;
        cameraPuzzle.GetComponent<Camera>().enabled = false;
        cam.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (manos) manos.SetActive(true);
        if (playerMovement) playerMovement.enabled = true;
        if (mouseLook) mouseLook.enabled = true;
    }
}