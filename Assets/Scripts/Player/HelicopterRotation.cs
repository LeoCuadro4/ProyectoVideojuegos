
using UnityEngine;

public class HelicopterRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    private Rigidbody2D rb;
    private float rotationDirection = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float rotation = rb.rotation +
            rotationSpeed * rotationDirection * Time.fixedDeltaTime;

        rb.MoveRotation(rotation);
    }

    public void SetReverseRotation(bool reverse)
    {
        rotationDirection = reverse ? -1f : 1f;
    }
}

