using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PLayer Compnent References")]
    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;
    private float currentSpeed;
    private bool isSprinting = false;


    private InputAction m_moveAction;
    private InputAction m_sprintAction;
    private InputAction m_interactAction;

    private Vector2 moveInput;
    private Animator m_animator;
    private Rigidbody2D rb;

    public InputActionAsset InputActions;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        currentSpeed = speed;
    }


    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_sprintAction = InputSystem.actions.FindAction("Sprint");

    }

    private void Update()
    {
        rb.linearVelocity = moveInput * currentSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            m_animator.SetBool("isWalking", false);
            m_animator.SetFloat("LastInputX", moveInput.x);
            m_animator.SetFloat("LastInputY", moveInput.y);
        }
        
        moveInput = context.ReadValue<Vector2>();
        m_animator.SetFloat("InputX", moveInput.x);
        m_animator.SetFloat("InputY", moveInput.y);
    }

}
