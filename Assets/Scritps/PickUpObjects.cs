using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;

    [Header("Mano Derecha")]
    public Transform handHoldPoint;

    [Header("Configuración de Inspección")]
    public float rotateSpeed = 10f;
    private bool isInspecting = false;

    void Update()
    {
        if (PickedObject == null)
        {
            if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickableObject>().isPickable)
            {
                if (Input.GetKeyDown(KeyCode.F)) PickUp();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z)) Drop();

            HandleInspection();
        }
    }

    void HandleInspection()
    {
        if (Input.GetMouseButton(1))
        {
            isInspecting = true;
            float h = rotateSpeed * Input.GetAxis("Mouse X");
            float v = rotateSpeed * Input.GetAxis("Mouse Y");

            PickedObject.transform.Rotate(Vector3.up, -h, Space.World);
            PickedObject.transform.Rotate(Vector3.right, v, Space.World);
        }
        else if (isInspecting)
        {
            PickedObject.transform.localRotation = Quaternion.Slerp(
                PickedObject.transform.localRotation,
                Quaternion.identity,
                Time.deltaTime * 5f
            );

            if (Quaternion.Angle(PickedObject.transform.localRotation, Quaternion.identity) < 0.1f)
            {
                isInspecting = false;
            }
        }
    }

    void PickUp()
    {
        PickedObject = ObjectToPickUp;
        PickableObject objectScript = PickedObject.GetComponent<PickableObject>();
        objectScript.isPickable = false;

        Rigidbody rb = PickedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        PickedObject.transform.SetParent(handHoldPoint);

        PickedObject.transform.localPosition = objectScript.positionOffset;
        PickedObject.transform.localRotation = Quaternion.Euler(objectScript.rotationOffset);
    }

    void Drop()
    {
        PickedObject.GetComponent<PickableObject>().isPickable = true;
        PickedObject.transform.SetParent(null);

        Rigidbody rb = PickedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        PickedObject = null;
    }
}