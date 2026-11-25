

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    private void Update()
    {
        movementInput = Vector2.zero;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            movementInput.y = 1;

        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            movementInput.y = -1;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movementInput.x = -1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movementInput.x = 1;

        movementInput = movementInput.normalized;
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;
        
        Vector2 newPosition = rb.position +
            movementInput * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }
}

