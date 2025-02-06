using UnityEngine;

namespace WellnessCenter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } // Singleton instance

        [Header("References")]
        [SerializeField] private PlayerMovement player1;
        [SerializeField] private PlayerMovement player2;
        [SerializeField] private Data.AIExtrapolation dataHandler;
        [SerializeField] private AI.AIExtrapolation aiHandler;

        private bool isInitialized = false;

        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            if (player1 == null || player2 == null || dataHandler == null || aiHandler == null)
            {
                Debug.LogError("GameManager: Missing references. Please assign all dependencies in the Inspector.");
                return;
            }

            isInitialized = true;
            Debug.Log("GameManager: Initialization complete. Game is ready.");
        }

        public void LogPlayerInteraction(Vector3 position, string interactionType)
        {
            if (!isInitialized) return;

            Debug.Log($"GameManager: Logging interaction at {position} - Type: {interactionType}");
            dataHandler.LogInteraction(position, interactionType);
        }

        public void SendDataToAI()
        {
            if (!isInitialized) return;

            Debug.Log("GameManager: Preparing data for AI...");
            string jsonData = dataHandler.PrepareDataForAI();
            aiHandler.SendDataToAI(jsonData);
        }

        public void UpdatePlayerPositions()
        {
            if (!isInitialized) return;

            // Example: Triggering interactions based on player proximity
            float distance = Vector3.Distance(player1.transform.position, player2.transform.position);

            if (distance < 2.0f) // Example interaction threshold
            {
                Debug.Log("GameManager: Players are close enough for interaction.");
                LogPlayerInteraction(player1.transform.position, "PlayerInteraction");
            }
        }

        private void Update()
        {
            if (!isInitialized) return;

            // Continuously check and update game logic
            UpdatePlayerPositions();
        }
    }
}

