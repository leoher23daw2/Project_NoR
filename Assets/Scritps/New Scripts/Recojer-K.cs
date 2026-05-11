using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] Transform holdPoint;
    [SerializeField] float pickupRange = 5f;
    [SerializeField] float throwForce = 6f;
    [SerializeField] KeyCode pickupKey = KeyCode.E;

    private Pickable heldObject;

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldObject != null)
                ThrowObject();
            else
                TryPickUp();
        }
    }

    void TryPickUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f)
        );

        RaycastHit[] hits = Physics.RaycastAll(ray, pickupRange);
        Debug.Log("Oggetti colpiti: " + hits.Length);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("- " + hit.collider.name + " layer: " + hit.collider.gameObject.layer);
            Pickable p = hit.collider.GetComponent<Pickable>();
            if (p != null)
            {
                heldObject = p;
                heldObject.PickUp(holdPoint);
                return;
            }
        }
    }

    void ThrowObject()
    {
        heldObject.Drop(transform.forward * throwForce);
        heldObject = null;
    }
}