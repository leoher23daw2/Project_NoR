using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [Header("UI")]
    public GameObject zainoPanel;
    public Image[] slots;

    private Sprite[] oggetti = new Sprite[5];
    private bool aperto = false;

    void Start()
    {
        zainoPanel.SetActive(false); // parte chiuso
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            aperto = !aperto;
            zainoPanel.SetActive(aperto);
            Cursor.lockState = aperto ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = aperto;
        }
    }
    public bool AggiungiOggetto(Sprite icona)
    {
        for (int i = 0; i < oggetti.Length; i++)
        {
            if (oggetti[i] == null)
            {
                oggetti[i] = icona;
                slots[i].sprite = icona;
                slots[i].color = Color.white;
                return true;
            }
        }
        Debug.Log("Zaino pieno!");
        return false;
    }


}