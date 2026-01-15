using UnityEngine;

public class Rotator : MonoBehaviour
{
    public enum RotationSpace
    {
        Local,
        World
    }

    [SerializeField]
    private Vector3 rotationSpeed = new Vector3(0f, 90f, 0f); // degrees per second
    [SerializeField] private RotationSpace rotationSpace = RotationSpace.World;

    private void Update()
    {
        Space space = rotationSpace == RotationSpace.World ? Space.World : Space.Self;
        transform.Rotate(rotationSpeed * Time.deltaTime, space);
    }
}