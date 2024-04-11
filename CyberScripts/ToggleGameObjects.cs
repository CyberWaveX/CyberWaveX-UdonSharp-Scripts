using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ToggleGameObjects : UdonSharpBehaviour
{
    [Header("GameObjects to Toggle")]
    [SerializeField]
    private GameObject[] toggleObjects;

    [Header("Toggle State")]
    [UdonSynced(UdonSyncMode.None)]
    private bool isToggled = false;

    public override void Interact()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ToggleObjects));
    }

    public void ToggleObjects()
    {
        // Check ownership before toggling objects
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }

        isToggled = !isToggled;

        foreach (GameObject obj in toggleObjects)
        {
            if (obj != null)
            {
                obj.SetActive(isToggled);
            }
        }
    }
}
