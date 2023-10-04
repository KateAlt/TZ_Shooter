using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private CharacterController controller;
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private float speed = 5f;

    private Vector2 MoveComposite;
    private Animator animator;
    private Vector3 moveDirection;
    private Vector3 velocity;

    public BaseData baseData;
   

    //----Jump
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.8f;

    private bool isJumping = false;
    private float verticalVelocity = 0f;


    private void Awake()
    {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Move.performed += OnMovePerformed;
        playerControls.Player.Move.canceled += OnMoveCanceled;
        playerControls.Player.Jump.performed += OnJumpPerformed;
        playerControls.Player.Fire.performed += OnFirePerformed;
    }

    private void OnDisable()
    {
        playerControls.Player.Move.performed -= OnMovePerformed;
        playerControls.Player.Move.canceled -= OnMoveCanceled;
        playerControls.Player.Jump.performed -= OnJumpPerformed;
        playerControls.Player.Fire.performed -= OnFirePerformed;
        playerControls.Disable();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (controller.isGrounded)
        {
            isJumping = true;
            verticalVelocity = jumpForce;
        }

        animator.SetBool("IsJumping", true);
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("I'm fire");
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveComposite = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveComposite = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Movement();

        if (!controller.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity = -0.1f;
        }

        Vector3 verticalMovement = new Vector3(0f, verticalVelocity, 0f);
        controller.Move(verticalMovement * Time.deltaTime);

        if (controller.isGrounded)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }


    private void Movement()
    {
        Quaternion cameraRotation = freeLookCamera.GetRig(0).transform.rotation;

        Vector3 cameraForward = cameraRotation * Vector3.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraRotation * Vector3.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        moveDirection = cameraForward * MoveComposite.y + cameraRight * MoveComposite.x;
        velocity = moveDirection.normalized * speed;

        if (controller.isGrounded)
        {
            if (moveDirection != Vector3.zero)
            {
                animator.SetBool("IsRunning", true);
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, Time.deltaTime * 5.0f);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void OnDestroy()
    {
        if (baseData != null)
        {
            baseData.healthPlayer = 100;
            baseData.healthEnemy = 100;
        }
    }
}