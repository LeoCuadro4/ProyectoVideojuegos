

using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Retroceso")]
    [SerializeField] private float knockbackDistance = 0.25f;
    [SerializeField] private float knockbackDuration = 0.15f;

    [Header("Rotacion")]
    [SerializeField] private float reverseRotationDuration = 0.15f;

    [Header("Efecto visual")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float hitColorDuration = 0.12f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private HelicopterRotation helicopterRotation;
    private PlayerMovement playerMovement;

    private Color originalColor;
    private bool isColliding;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        helicopterRotation = GetComponent<HelicopterRotation>();
        playerMovement = GetComponent<PlayerMovement>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding)
            return;

        isColliding = true;

        Vector2 impactDirection =
            ((Vector2)transform.position - (Vector2)other.ClosestPoint(transform.position)).normalized;

        StartCoroutine(ImpactEffect(impactDirection));
    }

    private IEnumerator ImpactEffect(Vector2 direction)
    {
        playerMovement.SetCanMove(false);

        if (spriteRenderer != null)
            spriteRenderer.color = hitColor;

        helicopterRotation.SetReverseRotation(true);

        Vector2 startPosition = rb.position;
        Vector2 targetPosition = startPosition + direction * knockbackDistance;

        float elapsed = 0f;

        while (elapsed < knockbackDuration)
        {
            elapsed += Time.fixedDeltaTime;

            float progress = Mathf.Clamp01(elapsed / knockbackDuration);

            rb.MovePosition(Vector2.Lerp(
                startPosition,
                targetPosition,
                progress
            ));

            yield return new WaitForFixedUpdate();
        }

        helicopterRotation.SetReverseRotation(false);

        float remainingColorTime =
            Mathf.Max(0f, hitColorDuration - knockbackDuration);

        yield return new WaitForSeconds(remainingColorTime);

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        playerMovement.SetCanMove(true);
        isColliding = false;
    }
}

