using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce = 8f;
    public float fallMultiplier = 2f;
    public float riseMultiplier = 2f;
    public float forwardSpeed = 15f;

    [Header("Lane Movement")]
    public int laneCount = 3;
    public float laneWidth = 3.33f; //platform width divided by number of lanes
    public float laneChangeSpeed = 10f;

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
    private int currentLane = 1; // Start in center lane (0: left, 1: center, 2: right)

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
        HandleLaneInput();
        HandleJump();
        UpdateInvincibility();
    }

    void FixedUpdate()
    {
        HandleMovement();
        BetterJump();
        CheckGround();
    }

    void HandleLaneInput()
    {
        if (!canControl) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeLane(-1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ChangeLane(1);
    }

    void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, laneCount - 1);
    }

    void HandleMovement()
    {
        if (!canControl) { return; }

        float targetX = GetLaneXPosition(currentLane);

        Vector3 position = rb.position;
        position.x = Mathf.Lerp(position.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);
        position.z += forwardSpeed * Time.fixedDeltaTime;

        rb.MovePosition(position);
    }

    float GetLaneXPosition(int laneIndex)
    {
        float halfWidth = (laneCount - 1) * laneWidth * 0.5f;
        return (laneIndex * laneWidth) - halfWidth;
    }

    void OnDrawGizmos()
{
    Gizmos.color = Color.yellow;

    for (int i = 0; i < laneCount; i++)
    {
        float x = GetLaneXPosition(i);
        Gizmos.DrawLine(
            new Vector3(x, 0, transform.position.z - 20),
            new Vector3(x, 0, transform.position.z + 20)
        );
    }
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
