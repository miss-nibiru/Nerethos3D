using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float mouseSensitivity = 80f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 1.1f;

    private Rigidbody _rb;
    private Vector3 _movement;
    private float _cameraYaw;
    private bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        // Keeps the capsule from falling over when it hits walls/stairs.
        _rb.freezeRotation = true;

        // Makes mouse look work properly in Game view.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTarget != null)
        {
            _cameraYaw = cameraTarget.eulerAngles.y;
        }
    }

    private void Update()
    {
        HandleCameraRotation();
        HandleMovementInput();
        HandlePlayerRotation();
        CheckGrounded();
        HandleJump();
        HandleCursorUnlock();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = _rb.position + _movement * (moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }

    private void HandleCameraRotation()
    {
        if (cameraTarget == null)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        _cameraYaw += mouseX * mouseSensitivity * Time.deltaTime;

        cameraTarget.rotation = Quaternion.Euler(0f, _cameraYaw, 0f);
    }

    private void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        if (cameraTarget == null)
        {
            _movement = new Vector3(moveX, 0f, moveZ).normalized;
            return;
        }

        Vector3 cameraForward = cameraTarget.forward;
        Vector3 cameraRight = cameraTarget.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        _movement = (cameraRight * moveX + cameraForward * moveZ).normalized;
    }

    private void HandlePlayerRotation()
    {
        if (_movement.sqrMagnitude <= 0.01f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(_movement);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleCursorUnlock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
