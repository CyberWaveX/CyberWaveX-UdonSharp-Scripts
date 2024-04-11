using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TrampolinePad : UdonSharpBehaviour
{
    [Header("Jump Pad Settings")]
    [Tooltip("The power of the jump. Adjust this value to increase or decrease the jump height.")]
    public float JumpPower = 10.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            VRCPlayerApi player = VRCPlayerApi.GetPlayerByGameObject(other.gameObject);
            if (player != null && player.isLocal)
            {
                Vector3 jumpDirection = transform.up; // This assumes the jump pad faces upwards. Adjust as needed.
                player.SetVelocity(jumpDirection * JumpPower);
            }
        }
    }
}

// CyberWaveX Copyright 2024 
// Please credit when using this code.