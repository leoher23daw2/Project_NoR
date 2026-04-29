using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [Header("Configuración de Objetos")]
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform interactionZone;

    [Header("Posición en la Mano")]
    public Vector3 handOffset = new Vector3(0.5f, -0.1f, 1.0f);

    [Header("Movimiento y Peso")]
    public float followSpeed = 10f;
    public float dragAmount = 1.5f; 

    [Header("Inspección")]
    public float rotateSpeed = 10f;
    private Quaternion freeRotation;

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
            HandleObjectLogic();
        }
    }

    void HandleObjectLogic()
    {
        Vector3 targetPos = transform.position
                            + (transform.forward * handOffset.z)
                            + (transform.right * handOffset.x)
                            + (transform.up * handOffset.y);

        float mouseX = Input.GetAxis("Mouse X") * dragAmount;
        float mouseY = Input.GetAxis("Mouse Y") * dragAmount;
        Vector3 swayOffset = (transform.right * -mouseX) + (transform.up * -mouseY);

        PickedObject.transform.position = Vector3.Lerp(
            PickedObject.transform.position,
            targetPos + (swayOffset * 0.01f),
            Time.deltaTime * followSpeed
        );

        if (Input.GetMouseButton(1)) 
        {
            float h = rotateSpeed * Input.GetAxis("Mouse X");
            float v = rotateSpeed * Input.GetAxis("Mouse Y");

            PickedObject.transform.Rotate(transform.up, -h, Space.World);
            PickedObject.transform.Rotate(transform.right, v, Space.World);

            freeRotation = Quaternion.Inverse(transform.rotation) * PickedObject.transform.rotation;
        }
        else
        {
            PickedObject.transform.rotation = Quaternion.Slerp(
                PickedObject.transform.rotation,
                transform.rotation * freeRotation,
                Time.deltaTime * followSpeed
            );
        }
    }

    void PickUp()
    {
        PickedObject = ObjectToPickUp;
        PickedObject.GetComponent<PickableObject>().isPickable = false;

        freeRotation = Quaternion.Inverse(transform.rotation) * PickedObject.transform.rotation;

        Rigidbody rb = PickedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Drop()
    {
        PickedObject.GetComponent<PickableObject>().isPickable = true;
        Rigidbody rb = PickedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        PickedObject = null;
    }
}