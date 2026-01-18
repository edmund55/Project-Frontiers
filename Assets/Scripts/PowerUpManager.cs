using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    [Header("Durations")]
    [SerializeField] private float doubleScoreDuration = 5f;
    [SerializeField] private float shieldDuration = 5f;

    [Header("Visuals")]
    [SerializeField] private GameObject cheeseEffect;
    private GameObject currentCheeseEffect;
    [SerializeField] private GameObject lightningEffect;
    private GameObject currentLightningEffect;

    [Header("UI")]
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Slider doubleScoreSlider;

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

    private void Start()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player == null) return;

        shieldSlider.gameObject.SetActive(false);
        doubleScoreSlider.gameObject.SetActive(false);
    }

    // Life
    public void GiveLife(Player player, int amount)
    {
        player.AddHealth(amount);
    }

    // Double Score
    public void ActivateDoubleScore(Player player)
    {
        if (doubleScoreRoutine != null)
            StopCoroutine(doubleScoreRoutine);

        doubleScoreRoutine = StartCoroutine(DoubleScoreCoroutine(player));
    }

    private IEnumerator DoubleScoreCoroutine(Player player)
    {
        doubleScoreActive = true;

        currentLightningEffect = Instantiate(lightningEffect, player.transform);

        float timeleft = doubleScoreDuration;
        doubleScoreSlider.gameObject.SetActive(true);

        while (timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            doubleScoreSlider.value = timeleft / doubleScoreDuration;
            yield return null;
        }

        Destroy(currentLightningEffect);
        doubleScoreSlider.gameObject.SetActive(false);

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

        currentCheeseEffect = Instantiate(cheeseEffect, player.transform);

        float timeLeft = shieldDuration;
        shieldSlider.gameObject.SetActive(true);

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            shieldSlider.value = timeLeft / shieldDuration;
            yield return null;
        }

        Destroy(currentCheeseEffect);
        shieldSlider.gameObject.SetActive(false);

        shieldActive = false;
        player.SetShield(false);
    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }
}
