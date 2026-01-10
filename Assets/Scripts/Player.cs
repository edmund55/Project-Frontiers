using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 15f;

    [Header("Lane Movement")]
    public int laneCount = 3;
    public float laneWidth = 3.33f; //platform width divided by number of lanes
    public float laneChangeSpeed = 10f;

    [Header("Health")]
    public int maxHealth = 3;

    [Header("Invincibility")]
    public float invincibilityDuration = 2f;

    [Header("Visuals")]
    private MeshRenderer[] playerRenderers; // player mesh; visual during invicibility

    [Header("Battery System")]
    public float maxBattery = 100f;
    public float batteryDecreasePerSecond = 10f;
    public Slider batteryBar;
    private float currentBattery;


    private bool canControl = true;
    private int currentHealth;
    public Slider healthBar;
    private bool isDead;
    private bool isInvincible = false;
    private float invincibilityTimer;
    private int currentLane = 1; // Start in center lane (0: left, 1: center, 2: right)

    void Awake()
    {
        playerRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentBattery = maxBattery;

        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (batteryBar != null)
        {
            batteryBar.minValue = 0;
            batteryBar.maxValue = maxBattery;
            batteryBar.value = currentBattery;
        }
    }

    void Update()
    {
        if (isDead) return;

        HandleForwardMovement();
        HandleLaneInput();
        HandleLaneMovement();
        UpdateInvincibility();
        HandleBattery();
    }

    void HandleForwardMovement()
    {
        if (!canControl) { return; }

        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);
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
    void HandleLaneMovement()
    {
        float targetX = GetLaneXPosition(currentLane);

        Vector3 position = transform.position;
        position.x = Mathf.Lerp(position.x, targetX, laneChangeSpeed * Time.deltaTime);

        transform.position = position;
    }
    float GetLaneXPosition(int laneIndex)
    {
        float halfWidth = (laneCount - 1) * laneWidth * 0.5f;
        return (laneIndex * laneWidth) - halfWidth;
    }

    void HandleBattery()
    {
        currentBattery -= batteryDecreasePerSecond * Time.deltaTime;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

        UpdateBatteryUI();

        if (currentBattery <= 0)
        {
            Die();
        }
    }

    public void RechargeBattery(float amount)
    {
        if (isDead) return;
        
        currentBattery = Mathf.Clamp(currentBattery + amount, 0, maxBattery);
        UpdateBatteryUI();
    }

    void UpdateBatteryUI()
    {
        if (batteryBar != null)
        {
            batteryBar.value = currentBattery;
        }
    }

    void UpdateInvincibility()
    {
        if (!isInvincible) { return; }

        invincibilityTimer -= Time.deltaTime;

        // flicker effect during invicibility
        if (playerRenderers != null)
        {
            bool isVisible = (Mathf.Repeat(Time.time * 5f, 1f) > 0.5f);
            SetRenderersEnabled(isVisible);
        }

        if (invincibilityTimer <= 0f && isInvincible)
        {
            isInvincible = false;
            SetRenderersEnabled(true);
        }
    }

    void SetRenderersEnabled(bool state)
    {
        foreach (MeshRenderer renderer in playerRenderers)
        {
            if (renderer != null) renderer.enabled = state;
        }
    }


    public bool TakeDamage(int damage)
    {
        if (isDead || isInvincible)
            return false;

        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        ActivateInvincibility();
        return false;
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
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

        transform.Translate(Vector3.zero);
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

}
