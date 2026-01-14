using UnityEngine;

public class RotateY : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 90f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}