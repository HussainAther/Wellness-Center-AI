using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public void InteractWithPlayer(GameObject otherPlayer)
    {
        Debug.Log($"Interacted with {otherPlayer.name}");
        // Add logic for player-to-player interaction
    }

    public void InteractWithEnvironment(GameObject environmentObject)
    {
        Debug.Log($"Interacted with {environmentObject.name}");
        // Add logic for environment interaction
    }
}

