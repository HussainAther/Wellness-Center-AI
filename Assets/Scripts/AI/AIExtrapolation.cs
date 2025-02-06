using UnityEngine;
using UnityEngine.Networking;

namespace WellnessCenter.AI
{
    public class AIExtrapolation : MonoBehaviour
    {
        [SerializeField] private string aiEndpoint = "http://example.com/api/analyze";

        public void SendDataToAI(string jsonData)
        {
            StartCoroutine(SendDataCoroutine(jsonData));
        }

        private IEnumerator SendDataCoroutine(string jsonData)
        {
            using (UnityWebRequest www = new UnityWebRequest(aiEndpoint, "POST"))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                www.uploadHandler = new UploadHandlerRaw(jsonToSend);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Data sent successfully to AI: " + www.downloadHandler.text);
                    ProcessAIResponse(www.downloadHandler.text);
                }
                else
                {
                    Debug.LogError("Failed to send data to AI: " + www.error);
                }
            }
        }

        private void ProcessAIResponse(string response)
        {
            Debug.Log("AI Response: " + response);
            // Handle AI response (e.g., update game state, generate feedback)
        }
    }
}

