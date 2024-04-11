using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class JoinSoundEffect : UdonSharpBehaviour
{
    public AudioSource audioSource; // Assign an AudioSource in the inspector
    public AudioClip joinSound; // Assign the sound you want to play on player join

    void Start()
    {
        // Ensure the AudioSource is not playing on start and loop is false
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        // Check if the joined player is not the local player to prevent the sound from playing when you join
        if (!player.isLocal)
        {
            PlayJoinSound();
        }
    }

    public void PlayJoinSound()
    {
        if (audioSource != null && joinSound != null)
        {
            audioSource.clip = joinSound;
            audioSource.Play();
        }
    }
}
