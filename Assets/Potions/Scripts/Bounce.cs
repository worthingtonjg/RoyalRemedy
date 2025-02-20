using UnityEngine;
using UnityEngine.Rendering;

public class Bounce : MonoBehaviour
{
    private Vector3 initialPosition;

    public float bounceHeight = 0.5f; // adjust this value to change the bounce height
    public float bounceSpeed = 1.0f; // adjust this value to change the bounce speed

    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = initialPosition + new Vector3(0, offset, 0);
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}

