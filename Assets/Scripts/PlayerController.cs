using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationDamping = 10f;

    private Vector2 moveInput;
    private Vector3 movement;
    private float verticalVelocity;

    private CharacterController characterController;

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();

        ApplyRotation();

        ApplyMovement();
    }

    private void ApplyMovement()
    {
        characterController.Move(movement * speed * Time.deltaTime);
    }

    private void ApplyRotation()
    {
        if (moveInput == Vector2.zero) return;

        Vector3 horizontalMovement = new Vector3(movement.x, 0, movement.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(horizontalMovement), Time.deltaTime * rotationDamping);
    }

    private void ApplyGravity()
    {
        if (verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        movement.y = verticalVelocity;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        movement = new Vector3(moveInput.x, 0, moveInput.y);
        movement = movement.normalized;
    }
}
