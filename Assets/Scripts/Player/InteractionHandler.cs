using UnityEngine;

namespace WellnessCenter
{
    public class InteractionHandler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float interactionRange = 2.0f; // Range within which interaction occurs

        public delegate void InteractionEvent(Vector3 position, string interactionType);
        public static event InteractionEvent OnInteraction;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"InteractionHandler: Interaction triggered at {transform.position}");
                OnInteraction?.Invoke(transform.position, "EnvironmentInteraction");
            }
        }
    }
}

