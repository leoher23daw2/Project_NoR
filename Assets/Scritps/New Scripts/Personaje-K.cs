using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Referencia a la cámara")]
    public Transform cameraTransform;

    [Header("Crouch")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float altezzaNormale = 2f;
    public float altezzaAccovacciata = 1f;
    public float velocitaTransizione = 10f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float altezzaTarget;
    private float cameraNormale;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        altezzaTarget = altezzaNormale;
        cameraNormale = cameraTransform.localPosition.y;
    }

    void Update()
    {
        altezzaTarget = Input.GetKey(crouchKey) ? altezzaAccovacciata : altezzaNormale;
        controller.height = Mathf.Lerp(controller.height, altezzaTarget, Time.deltaTime * velocitaTransizione);

        float cameraTarget = cameraNormale - (altezzaNormale - controller.height) / 2f;
        Vector3 camPos = cameraTransform.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, cameraTarget, Time.deltaTime * velocitaTransizione);
        cameraTransform.localPosition = camPos;

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 move = cameraTransform.right * x + cameraTransform.forward * z;
        move.y = 0f;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}