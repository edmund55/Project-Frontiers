using UnityEngine;

public enum BulletOwner
{
    Player,
    Enemy
}

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public int damage = 1;
    public float lifeTime = 5f;

    public BulletOwner owner;
    public AudioClip bulletCrashClip;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bullet vs Bullet
        Bullet otherBullet = other.GetComponent<Bullet>();
        if (otherBullet != null && otherBullet.owner != owner)
        {
            AudioSource.PlayClipAtPoint(bulletCrashClip, transform.position);
            Destroy(otherBullet.gameObject);
            Destroy(gameObject);
            return;
        }

        // Player bullet
        if (owner == BulletOwner.Player)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyShooter>().Die();
                Destroy(gameObject);
            }
            else if (other.CompareTag("Obstacle"))
            {
                other.GetComponent<Obstacle>().PlayBulletCrashSound();
                Destroy(gameObject);
            }
        }

        // Enemy bullet
        if (owner == BulletOwner.Enemy)
        {
            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}
