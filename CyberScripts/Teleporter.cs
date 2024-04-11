using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Teleporter : UdonSharpBehaviour
{
    [Header("Target Location")]
    public Transform targetLocation; // Set this to the GameObject's transform you want to teleport players to.

    public override void Interact()
    {
        // Getting the local player
        VRCPlayerApi player = Networking.LocalPlayer;
        if (player == null) return;

        // Teleporting the player to the target location's position and rotation
        player.TeleportTo(targetLocation.position, targetLocation.rotation);
    }
}
