
using UnityEngine;

public class HelicopterRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float rotation = rb.rotation + rotationSpeed * Time.fixedDeltaTime;

        rb.MoveRotation(rotation);
    }
}

