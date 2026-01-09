using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Score Value")]
    [SerializeField] private int scoreValue = 1;

    [Header("Speed Control")]
    [SerializeField] private int scoreThreshold = 20;
    [SerializeField] private float speedIncrease = 5f;
    [SerializeField] private float maxSpeed = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        int previousScore = ScoreManager.Instance.GetScore(); // Get score before adding

        ScoreManager.Instance.AddScore(scoreValue);

        int currentScore = ScoreManager.Instance.GetScore(); // Get score after adding

        // calculate speed increases based on score thresholds
        int previousLevel = previousScore / scoreThreshold;
        int currentLevel = currentScore / scoreThreshold;
        int levelsGained = currentLevel - previousLevel;

        if (levelsGained > 0)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.forwardSpeed = Mathf.Min(player.forwardSpeed + speedIncrease * levelsGained, maxSpeed);
            }
        }

        gameObject.SetActive(false);
    }
}
