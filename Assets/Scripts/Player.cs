using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Forward Movement")]
    public float forwardSpeed = 15f;

    [Header("Lane Movement")]
    public int laneCount = 3;
    public float laneWidth = 4f; // platform width divided by number of lanes
    public float laneChangeSpeed = 10f;

    [Header("Vertical Movement")]
    public float verticalSpeed = 6f;
    public float minHeight = 1.5f;
    public float maxHeight = 5.5f; 

    [Header("Health")]
    public int maxHealth = 3;
    public AudioClip damageClip;
    public AudioClip deathClip;
    private bool shieldActive;

    [Header("Battery System")]
    public float maxBattery = 100f;
    public float batteryDecreasePerSecond = 5f;

    [Header("Invincibility")]
    public float invincibilityDuration = 2f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.3f;
    public AudioClip shootClip;

    [Header("Visuals")]
    private MeshRenderer[] playerRenderers; // player mesh; visual during invicibility
    public GameObject playerDamageEffect;
    public GameObject playerExplosionEffect;
    public GameObject playerSparkEffect;
    public GameObject playerSmokeEffect;

    private int currentHealth;
    private float currentBattery;

    private int currentLane = 1; // Start in center lane (0: left, 1: center, 2: right)
    private bool canControl = true;
    private bool isInvincible = false;
    private float invincibilityTimer;
    private float fireTimer;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentBattery = maxBattery;
    }

    void Update()
    {
        if (!canControl) return;

        HandleForwardMovement();
        HandleVerticalMovement();
        HandleLaneInput();
        HandleLaneMovement();
        HandleBattery();
        UpdateInvincibility();
        HandleShooting();
    }

    // Forward Movement
    void HandleForwardMovement()
    {
        if (!canControl) { return; }

        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);
    }

    // Lane Movement
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

    // Vertical Movement
    void HandleVerticalMovement()
    {
        float yInput = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            yInput = 1f;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            yInput = -1f;

        if (yInput == 0f) return;

        Vector3 position = transform.position;
        position.y += yInput * verticalSpeed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, minHeight, maxHeight);

        transform.position = position;
    }

    // Health System
    public bool TakeDamage(int damage)
    {
        if (!canControl || isInvincible || shieldActive)
            return false;

        if (damageClip != null)
            audioSource.PlayOneShot(damageClip);

        Instantiate(playerDamageEffect, transform);
        currentHealth -= damage;
        UIManager.Instance.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            DisableControl();
            Instantiate(playerExplosionEffect, transform);
            Instantiate(playerSparkEffect, transform.position, Quaternion.identity);
            Instantiate(playerSmokeEffect, transform.position, Quaternion.identity);

            return true;
        }

        ActivateInvincibility();
        return false;
    }
    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIManager.Instance.SetHealth(currentHealth);
    }

    public void SetShield(bool state)
    {
        shieldActive = state;
    }


    // Battery System
    void HandleBattery()
    {
        currentBattery -= batteryDecreasePerSecond * Time.deltaTime;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);

        UIManager.Instance.SetBattery(currentBattery);

        if (currentBattery <= 0)
        {
            DisableControl();
        }
    }
    public void RechargeBattery(float amount)
    {
        currentBattery = Mathf.Clamp(currentBattery + amount, 0, maxBattery);
        UIManager.Instance.SetBattery(currentBattery);
    }

    // Invincibility
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
    public void ActivateInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }

    // Shooting
    void HandleShooting()
    {
        fireTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireCooldown;
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().owner = BulletOwner.Player;

        if (shootClip != null)
            audioSource.PlayOneShot(shootClip);
    }


    // Visuals
    void SetRenderersEnabled(bool state)
    {
        foreach (MeshRenderer renderer in playerRenderers)
        {
            if (renderer != null) renderer.enabled = state;
        }
    }
    void SpawnEffect(GameObject prefab)
    {
        GameObject newEffect = Instantiate(prefab, transform);
        //newEffect.transform.localPosition = Vector3.zero;
    }

    // 
    public void DisableControl()
    {
        canControl = false;

        transform.Translate(Vector3.zero);
        audioSource.PlayOneShot(deathClip);
    }

}
