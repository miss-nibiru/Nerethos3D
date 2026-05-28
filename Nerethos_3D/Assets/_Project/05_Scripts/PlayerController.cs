using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 5f;
    
    public float groundCheckDistance = 1f;
    private bool _isGrounded;

    private Rigidbody _rb;
    private Vector3 _movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        _movement = new Vector3(moveX, 0, moveZ).normalized;
        
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    void FixedUpdate()
    {
        Vector3 newPosition = _rb.position + _movement * (moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }
    
}
