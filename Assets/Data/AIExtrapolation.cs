using UnityEngine;
using System.Collections.Generic;

namespace WellnessCenter.Data
{
    public class AIExtrapolation : MonoBehaviour
    {
        private List<InteractionData> interactionLogs = new List<InteractionData>();

        public void LogInteraction(Vector3 position, string interactionType)
        {
            // Log the interaction data locally
            interactionLogs.Add(new InteractionData
            {
                Position = position,
                InteractionType = interactionType,
                Timestamp = System.DateTime.UtcNow
            });

            Debug.Log("Interaction logged: " + interactionType);
        }

        public string PrepareDataForAI()
        {
            // Convert logged data into JSON or a suitable format
            string jsonData = JsonUtility.ToJson(new InteractionLogWrapper { Logs = interactionLogs });
            Debug.Log("Data prepared for AI: " + jsonData);
            return jsonData;
        }

        [System.Serializable]
        private class InteractionData
        {
            public Vector3 Position;
            public string InteractionType;
            public System.DateTime Timestamp;
        }

        [System.Serializable]
        private class InteractionLogWrapper
        {
            public List<InteractionData> Logs;
        }
    }
}

