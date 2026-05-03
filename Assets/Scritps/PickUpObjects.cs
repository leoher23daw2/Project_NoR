using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform brazoDerechoControl;
    public Transform handHoldPoint;

    [Header("Ajustes de Posición")]
    public Vector3 offsetInspeccion = new Vector3(-0.2f, 0.1f, -0.1f);
    public float suavizado = 10f;

    private Vector3 posOriginalLocal;
    private Quaternion rotOriginalLocal;

    void Start()
    {
        if (brazoDerechoControl != null)
        {
            posOriginalLocal = brazoDerechoControl.localPosition;
            rotOriginalLocal = brazoDerechoControl.localRotation;
        }
    }

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
        }

        ManejarBrazo();
    }

    void ManejarBrazo()
    {
        if (brazoDerechoControl == null) return;

        if (PickedObject != null && Input.GetMouseButton(1))
        {
            brazoDerechoControl.localPosition = Vector3.Lerp(brazoDerechoControl.localPosition, posOriginalLocal + offsetInspeccion, Time.deltaTime * suavizado);
        }
        else
        {
            brazoDerechoControl.localPosition = Vector3.Lerp(brazoDerechoControl.localPosition, posOriginalLocal, Time.deltaTime * suavizado);
            brazoDerechoControl.localRotation = Quaternion.Slerp(brazoDerechoControl.localRotation, rotOriginalLocal, Time.deltaTime * suavizado);
        }
    }

    void PickUp()
    {
        PickedObject = ObjectToPickUp;
        PickableObject script = PickedObject.GetComponent<PickableObject>();
        script.isPickable = false;


        Rigidbody rb = PickedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero; 
        }

        Collider col = PickedObject.GetComponent<Collider>();
        if (col != null) col.enabled = false; 


        PickedObject.transform.SetParent(handHoldPoint, false);

        PickedObject.transform.localPosition = script.positionOffset;
        PickedObject.transform.localRotation = Quaternion.Euler(script.rotationOffset);

        PickedObject.transform.localScale = Vector3.one;

        Debug.Log("Objeto cogido: " + PickedObject.name);
    }

    void Drop()
    {
        PickedObject.GetComponent<PickableObject>().isPickable = true;

        Collider col = PickedObject.GetComponent<Collider>();
        if (col != null) col.enabled = true;

        PickedObject.transform.SetParent(null);

        Rigidbody rb = PickedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        PickedObject = null;
    }
}