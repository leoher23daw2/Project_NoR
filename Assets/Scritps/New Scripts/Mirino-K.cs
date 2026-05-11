using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public Image crosshairImage;
    public float rayDistance = 3f;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance)
            && hit.collider.CompareTag("Interactable"))
        {
            crosshairImage.color = Color.white;
            crosshairImage.transform.localScale = Vector3.one * 1.8f;
        }
        else
        {
            crosshairImage.color = new Color(1f, 1f, 1f, 0.45f);
            crosshairImage.transform.localScale = Vector3.one;
        }

    }
}