using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    
    private PlayerInputs playerInput;
    
    private Vector2 movement;

    private Rigidbody2D rb;

    private Animator animator;

    private void Awake()
    {
        playerInput = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInput.Enable();

        playerInput.Gameplay.Movement.performed += OnMovement;
        playerInput.Gameplay.Movement.canceled += OnMovement;
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);

        if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime); 
        //rb.velocity = _Movement * _Speed;
    }

    private void Update()
    {

        //animator.SetFloat("LastHorizontal", movement.x);
        //animator.SetFloat("LastVertical", movement.y);
    }
}
