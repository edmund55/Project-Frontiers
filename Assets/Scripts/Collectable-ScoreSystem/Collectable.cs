using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ScoreManager.Instance.AddScore(scoreValue);
        gameObject.SetActive(false);
    }
}
