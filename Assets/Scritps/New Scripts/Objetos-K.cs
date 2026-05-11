using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    [Header("Inventario")]
    public bool vaInInventario = false;
    public Sprite iconaInventario;

    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private bool raccolto = false;

    public void PickUp(Transform holdPoint)
    {
        if (raccolto) return;
        raccolto = true;

        if (vaInInventario)
        {
            Inventario inv = FindObjectOfType<Inventario>();
            if (inv != null && inv.AggiungiOggetto(iconaInventario))
                Destroy(gameObject);
        }
        else
        {
            rb.isKinematic = true;
            transform.SetParent(holdPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }

    public void Drop(Vector3 throwForce = default)
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(throwForce, ForceMode.Impulse);
    }

}