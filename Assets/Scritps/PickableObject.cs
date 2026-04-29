using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool isPickable = true;

    [Header("Ajustes de Agarre en Mano")]
    [Tooltip("Mueve el objeto respecto al centro de la mano")]
    public Vector3 positionOffset;

    [Tooltip("Rota el objeto para que encaje bien en los dedos")]
    public Vector3 rotationOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerInteractionZone"))
        {
            PickUpObjects player = other.GetComponentInParent<PickUpObjects>();
            if (player != null)
            {
                player.ObjectToPickUp = this.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerInteractionZone"))
        {
            PickUpObjects player = other.GetComponentInParent<PickUpObjects>();
            if (player != null && player.ObjectToPickUp == this.gameObject)
            {
                player.ObjectToPickUp = null;
            }
        }
    }
}