using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider batteryBar;

    private int score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Score
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
    public int GetScore()
    {
        return score;
    }

    // Health
    public void SetHealth(int health)
    {
        healthBar.value = health;
    }

    // Battery
    public void SetBattery(float battery)
    {
        batteryBar.value = battery;
    }
}
