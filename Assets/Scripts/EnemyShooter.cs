using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;

    [Header("Audio")]
    public AudioClip shootClip; // enemy shooting sound

    private float timer;
    private AudioSource audioSource;
    private FlexibleSoundPlayer soundPlayer; // enemy destroy sound

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        soundPlayer = GetComponent<FlexibleSoundPlayer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().owner = BulletOwner.Enemy;

        if (shootClip != null)
            audioSource.PlayOneShot(shootClip);
    }

    public void Die()
    {
        soundPlayer.Play();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        bool playerKilled = player.TakeDamage(1);

        if (playerKilled)
        {
            player.DisableControl();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
