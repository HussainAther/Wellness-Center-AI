using UnityEngine;

public class DataTracking : MonoBehaviour
{
    public Transform player1; // Reference to Player 1
    public Transform player2; // Reference to Player 2
    public float interactionRadius = 2.0f; // Interaction distance threshold

    private void Update()
    {
        // Track player movement data
        Vector3 player1Position = player1.position;
        Vector3 player2Position = player2.position;

        // Check if players are within interaction radius
        float distance = Vector3.Distance(player1Position, player2Position);
        if (distance <= interactionRadius)
        {
            Debug.Log($"Players are interacting! Distance: {distance}");
            ProcessInteractionData(player1Position, player2Position);
        }
    }

    private void ProcessInteractionData(Vector3 position1, Vector3 position2)
    {
        // Placeholder for AI-driven data processing logic
        Debug.Log($"Processing interaction data: Player1({position1}), Player2({position2})");

        // Example: Send data to an AI system or log it locally
    }
}

