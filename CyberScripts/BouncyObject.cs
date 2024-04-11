using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// Define a namespace if necessary
namespace VRChatBounce
{
    // The class name should match the file name for clarity
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)] // Manual sync mode for specific control over when data is synchronized
    public class BouncyObject : UdonSharpBehaviour
    {
        [Header("Bounce Settings")]
        [Tooltip("The force of the bounce when interacted with.")]
        public float bounceForce = 5.0f;

        [Header("Networking")]
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(IsBouncing))]
        private bool isBouncing;

        private Rigidbody objectRigidbody; // Reference to the object's Rigidbody component

        void Start()
        {
            // Ensure this object has a Rigidbody component
            objectRigidbody = GetComponent<Rigidbody>();
            if (objectRigidbody == null)
            {
                // Log an error if no Rigidbody is attached
                Debug.LogError("BouncyObject script requires a Rigidbody component to function.");
            }
        }

        public override void Interact()
        {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(NetworkedBounce));
        }

        public void NetworkedBounce()
        {
            // This method will be called on all instances, ensuring the bounce effect is synchronized
            isBouncing = true; // Set the synchronized variable to trigger the bounce across all clients
        }

        public bool IsBouncing
        {
            set
            {
                isBouncing = value;
                if (isBouncing)
                {
                    ApplyBounce();
                }
            }
            get => isBouncing;
        }

        private void ApplyBounce()
        {
            if (objectRigidbody != null)
            {
                // Apply the bounce force
                objectRigidbody.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
            }

            // Reset the flag so it can bounce again
            isBouncing = false;
        }
    }
}
