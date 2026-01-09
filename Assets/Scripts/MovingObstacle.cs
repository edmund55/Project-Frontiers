using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float moveDistance = 0.9f; //Calculation: 1 - (obstacle scale / parent scale)
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
        startLocalPos.x = 0;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance) - (moveDistance / 2);
        transform.localPosition = startLocalPos + new Vector3(offset, 0, 0);
    }
}