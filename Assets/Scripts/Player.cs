using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 8f;
    public float fallMultiplier = 2f;
    public float riseMultiplier = 2f;
    public float forwardSpeed = 15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Health")]
    public int maxHealth = 3;

    [Header("Invincibility")]
    public float invincibilityDuration = 2f;

    [Header("Visuals")]
    private MeshRenderer playerRenderer; // player mesh; visual during invicibility

    private Rigidbody rb;
    private bool isGrounded;
    private bool canControl = true;
    private int currentHealth;
    private bool isDead;
    private bool isInvincible = false;
    private float invincibilityTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleJump();
        UpdateInvincibility();
    }

    void FixedUpdate()
    {
        HandleMovement();
        BetterJump();
        CheckGround();
    }

    void HandleMovement()
    {
        if (!canControl) { return; }

        float xInput = Input.GetAxis("Horizontal");

        Vector3 velocity = rb.linearVelocity;
        velocity.x = xInput * moveSpeed;
        velocity.z = forwardSpeed;
        rb.linearVelocity = velocity;
    }

    void HandleJump()
    {
        if (!canControl) { return; }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (riseMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }
    void UpdateInvincibility()
    {
        if (!isInvincible) { return; }

        invincibilityTimer -= Time.deltaTime;

        // flicker effect during invicibility
        if (playerRenderer != null)
        {
            playerRenderer.enabled = (Mathf.Repeat(Time.time * 5f, 1f) > 0.5f);
        }

        if (invincibilityTimer <= 0f)
        {
            isInvincible = false;
            if (playerRenderer != null) playerRenderer.enabled = true;
        }
    }


    public bool TakeDamage(int damage)
    {
        if (isDead || isInvincible)
            return false;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        ActivateInvincibility();
        return false;
    }

    public void ActivateInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }


    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        DisableControl();
    }


    public void DisableControl()
    {
        canControl = false;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = 0f;
        velocity.z = 0f;
        rb.linearVelocity = velocity;
    }

}
