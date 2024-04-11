using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DoubleJump : UdonSharpBehaviour
{
    [Header("Jump Configuration")]
    [Tooltip("Enables or disables the triple jump feature.")]
    public bool enableTripleJump = false;
    [Tooltip("The force applied for each jump.")]
    public float jumpForce = 5.0f;

    private int jumpCount = 0;

    void Start()
    {
        // Ensure that the script uses the correct networking settings.
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
    }

    public override void InputJump(bool value, VRC.Udon.Common.UdonInputEventArgs args)
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        if (player == null) return;

        // Calculate maximum jump count based on whether triple jump is enabled.
        int maxJumpCount = enableTripleJump ? 3 : 2;

        // Check if the player is on the ground or if they haven't exceeded the jump limit.
        if (player.IsPlayerGrounded() || jumpCount < maxJumpCount)
        {
            if (value) // If the jump input is pressed.
            {
                player.SetVelocity(new Vector3(player.GetVelocity().x, jumpForce, player.GetVelocity().z));
                jumpCount++;

                // Reset jump count if the player is on the ground.
                if (player.IsPlayerGrounded())
                {
                    jumpCount = 0;
                }
            }
        }
    }

    public override void OnPlayerRespawn(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            // Reset the jump count when the player respawns.
            jumpCount = 0;
        }
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player.isLocal)
        {
            // Reset the jump count when the player lands on a trigger collider.
            jumpCount = 0;
        }
    }
}

// CyberWaveX Copyright 2024 
// Please credit when using this code.