using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference sprintAction;

    [Header("Movement")]
    [SerializeField, Min(0f)] private float walkSpeed = 4f;
    [SerializeField, Min(0f)] private float sprintSpeed = 6f;
    [SerializeField, Min(0f)] private float movementSharpness = 14f;
    [SerializeField] private float gravity = -25f;
    [SerializeField] private float groundedForce = -2f;

    [Header("First-Person Look")]
    [SerializeField, Min(0f)] private float mouseSensitivity = 0.08f;
    [SerializeField, Min(0f)] private float gamepadLookSpeed = 140f;
    [SerializeField, Range(1f, 89f)] private float maximumLookAngle = 85f;

    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private float interactionDistance = 3f;
    
    [Header("Cursor")]
    [SerializeField] private bool lockCursorOnEnable = true;

    private CharacterController _characterController;
    private Vector3 _horizontalVelocity;
    private float _verticalVelocity;
    private float _cameraPitch;
    private bool _inputEnabled = true;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (cameraPivot != null)
        {
            _cameraPitch = NormalizeAngle(cameraPivot.localEulerAngles.x);
            _cameraPitch = Mathf.Clamp(
                _cameraPitch,
                -maximumLookAngle,
                maximumLookAngle);
        }
    }

    private void OnEnable()
    {
        EnableAction(moveAction);
        EnableAction(lookAction);
        EnableAction(sprintAction);
        EnableAction(interactAction);

        if (lockCursorOnEnable)
        {
            LockCursor();
        }
    }

    private void OnDisable()
    {
        DisableAction(moveAction);
        DisableAction(lookAction);
        DisableAction(sprintAction);
        DisableAction(interactAction);
    }

    private void Update()
    {
        HandleCursor();

        if (_inputEnabled && Cursor.lockState == CursorLockMode.Locked)
        {
            HandleLook();
        }

        HandleMovement();
        HandleInteraction();
    }

    private void HandleLook()
    {
        if (cameraPivot == null || lookAction == null || lookAction.action == null)
        {
            return;
        }

        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        bool usingMouse = lookAction.action.activeControl?.device is Mouse;

        float lookMultiplier = usingMouse
            ? mouseSensitivity
            : gamepadLookSpeed * Time.deltaTime;

        float yaw = lookInput.x * lookMultiplier;
        float pitch = lookInput.y * lookMultiplier;

        transform.Rotate(Vector3.up, yaw, Space.Self);

        _cameraPitch = Mathf.Clamp(
            _cameraPitch - pitch,
            -maximumLookAngle,
            maximumLookAngle);

        cameraPivot.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
    }

    private void HandleMovement()
    {
        Vector2 moveInput = Vector2.zero;

        if (_inputEnabled && moveAction != null && moveAction.action != null)
        {
            moveInput = Vector2.ClampMagnitude(
                moveAction.action.ReadValue<Vector2>(),
                1f);
        }

        Vector3 desiredDirection =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        float targetSpeed = IsSprinting() ? sprintSpeed : walkSpeed;
        Vector3 desiredVelocity = desiredDirection * targetSpeed;

        float smoothing = 1f - Mathf.Exp(-movementSharpness * Time.deltaTime);
        _horizontalVelocity = Vector3.Lerp(
            _horizontalVelocity,
            desiredVelocity,
            smoothing);

        if (_characterController.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = groundedForce;
        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalVelocity = _horizontalVelocity;
        finalVelocity.y = _verticalVelocity;

        _characterController.Move(finalVelocity * Time.deltaTime);
    }
    
    private void HandleInteraction()
    {
        if (!_inputEnabled ||
            interactAction == null ||
            !interactAction.action.WasPressedThisFrame())
        {
            return;
        }

        if (Physics.Raycast(
                cameraPivot.position,
                cameraPivot.forward,
                out RaycastHit hit,
                interactionDistance))
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null && interactable.CanInteract())
            {
                interactable.Interact();
            }
        }
    }

    private bool IsSprinting()
    {
        return _inputEnabled &&
               sprintAction != null &&
               sprintAction.action != null &&
               sprintAction.action.IsPressed();
    }

    private void HandleCursor()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnlockCursor();
        }

        if (_inputEnabled &&
            Cursor.lockState != CursorLockMode.Locked &&
            Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            LockCursor();
        }
    }

    public void SetInputEnabled(bool enabled)
    {
        _inputEnabled = enabled;

        if (!enabled)
        {
            _horizontalVelocity = Vector3.zero;
            UnlockCursor();
            return;
        }

        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private static void EnableAction(InputActionReference actionReference)
    {
        actionReference?.action?.Enable();
    }

    private static void DisableAction(InputActionReference actionReference)
    {
        actionReference?.action?.Disable();
    }

    private static float NormalizeAngle(float angle)
    {
        return angle > 180f ? angle - 360f : angle;
    }
}
