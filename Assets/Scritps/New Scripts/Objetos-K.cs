using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    [Header("Inventario")]
    public bool vaInInventario = false;
    public Sprite iconaInventario;

    [Header("Posizione in mano")]
    public Vector3 offsetPosizione = Vector3.zero;
    public Vector3 offsetRotazione = Vector3.zero;

    private Rigidbody rb;
    private Collider col;
    private bool raccolto = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void PickUp(Transform holdPoint)
    {
        if (raccolto) return;
        raccolto = true;

        if (vaInInventario)
        {
            Inventario inv = FindFirstObjectByType<Inventario>();
            if (inv != null && inv.AggiungiOggetto(iconaInventario))
                Destroy(gameObject);
        }
        else
        {
            rb.isKinematic = true;
            Vector3 scalaOriginale = transform.lossyScale;
            transform.SetParent(holdPoint);
            transform.localPosition = offsetPosizione;
            transform.localRotation = Quaternion.Euler(offsetRotazione);
            transform.localScale = new Vector3(
                scalaOriginale.x / holdPoint.lossyScale.x,
                scalaOriginale.y / holdPoint.lossyScale.y,
                scalaOriginale.z / holdPoint.lossyScale.z
            );
        }
    }

    public void Drop(Vector3 throwForce = default)
    {
        raccolto = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(throwForce, ForceMode.Impulse);
    }
}