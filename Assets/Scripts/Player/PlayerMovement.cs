using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed of the player
    public float moveSpeed = 5f;

    // Reference to the Rigidbody component
    private Rigidbody rb;

    // Input variables
    private Vector3 movementInput;

    void Start()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Capture movement input
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveVertical = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow
        movementInput = new Vector3(moveHorizontal, 0, moveVertical).normalized;
    }

    void FixedUpdate()
    {
        // Apply movement
        MovePlayer();
    }

    void MovePlayer()
    {
        if (movementInput != Vector3.zero)
        {
            // Calculate the movement direction
            Vector3 move = movementInput * moveSpeed * Time.fixedDeltaTime;

            // Apply movement to the Rigidbody
            rb.MovePosition(rb.position + move);

            // Rotate player to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, 0.15f));
        }
    }
}

