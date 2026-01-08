using UnityEngine;
using TMPro;

public class UIscore : MonoBehaviour
{
    public static UIscore Instance;

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        Instance = this;
        UpdateScore(0);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
