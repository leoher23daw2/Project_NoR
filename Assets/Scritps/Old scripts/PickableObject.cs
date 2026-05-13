using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool isPickable = true;

    [Header("Ajuste en Mano")]
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerInteractionZone"))
            other.GetComponentInParent<PickUpObjects>().ObjectToPickUp = this.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerInteractionZone"))
            other.GetComponentInParent<PickUpObjects>().ObjectToPickUp = null;
    }
}