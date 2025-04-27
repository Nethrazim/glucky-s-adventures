using UnityEngine;


public class FloatingIsland : MonoBehaviour
{
    public float amplitude = 0.5f; // The height of the floating movement
    public float frequency = 1f; // The speed of the floating movement
    public float rotationSpeed = 10f; // The speed of the rotation
    public bool rotationRight = true;
    private Vector3 startPosition;
    public bool execute = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (execute)
        {
            // Floating movement
            float yOffset = amplitude * Mathf.Sin(Time.time * frequency);
            transform.position = new Vector3(startPosition.x, startPosition.y + yOffset, startPosition.z);

            if (rotationRight && rotationSpeed > 0.0f)
            {
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
            else if(!rotationRight && rotationSpeed > 0.0f)
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            }
        }
    }
}
