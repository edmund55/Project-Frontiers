using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    [Header("Durations")]
    [SerializeField] private float doubleScoreDuration = 5f;
    [SerializeField] private float shieldDuration = 5f;

    private bool doubleScoreActive;
    private bool shieldActive;

    private Coroutine doubleScoreRoutine;
    private Coroutine shieldRoutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Life
    public void GiveLife(Player player, int amount)
    {
        player.AddHealth(amount);
    }

    // Double Score
    public void ActivateDoubleScore()
    {
        if (doubleScoreRoutine != null)
            StopCoroutine(doubleScoreRoutine);

        doubleScoreRoutine = StartCoroutine(DoubleScoreCoroutine());
    }

    private IEnumerator DoubleScoreCoroutine()
    {
        doubleScoreActive = true;
        yield return new WaitForSeconds(doubleScoreDuration);
        doubleScoreActive = false;
    }

    public int ModifyScore(int baseScore)
    {
        return doubleScoreActive ? baseScore * 2 : baseScore;
    }

    // Shield
    public void ActivateShield(Player player)
    {
        if (shieldRoutine != null)
            StopCoroutine(shieldRoutine);

        shieldRoutine = StartCoroutine(ShieldCoroutine(player));
    }

    private IEnumerator ShieldCoroutine(Player player)
    {
        shieldActive = true;
        player.SetShield(true);

        yield return new WaitForSeconds(shieldDuration);

        shieldActive = false;
        player.SetShield(false);
    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }
}
