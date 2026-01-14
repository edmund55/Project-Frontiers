using UnityEngine;

public class CollectableStar : MonoBehaviour
{
    [Header("Score Value")]
    [SerializeField] private int scoreValue = 1;

    [Header("Speed Control")]
    [SerializeField] private int scoreThreshold = 20;
    [SerializeField] private float speedIncrease = 5f;
    [SerializeField] private float maxSpeed = 30f;

    private FlexibleSoundPlayer soundPlayer;
    private bool triggered = false;

    private void Awake()
    {
        soundPlayer = GetComponent<FlexibleSoundPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        triggered = true;

        soundPlayer.Play();

        int previousScore = UIManager.Instance.GetScore(); // Get score before adding

        int modifiedScore = PowerUpManager.Instance.ModifyScore(scoreValue);
        UIManager.Instance.AddScore(modifiedScore);

        int currentScore = UIManager.Instance.GetScore(); // Get score after adding

        // calculate speed increases based on score thresholds
        int previousLevel = previousScore / scoreThreshold;
        int currentLevel = currentScore / scoreThreshold;
        int levelsGained = currentLevel - previousLevel;

        if (levelsGained > 0)
        {
            player.forwardSpeed = Mathf.Min(player.forwardSpeed + (levelsGained * speedIncrease), maxSpeed);
        }

        Destroy(gameObject);
    }
}
