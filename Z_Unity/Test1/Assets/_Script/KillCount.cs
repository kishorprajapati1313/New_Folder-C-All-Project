using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class KillCount : NetworkBehaviour
{
    // [SyncVar] // Synchronize kill count across the network
    // public int killcount = 0;

    // // Method to increment kill count on the server
    // // [ServerRpc]
    // public void IncrementKill()
    // {
    //     if (IsServer)
    //     {
    //         killcount++;
    //         Debug.Log($"Kill count: {killcount}");

    //         // Synchronize kill count to all clients
    //         SyncKillCount();
    //     }
    // }

    // // Method to synchronize kill count to all clients
    // // [ClientRpc]
    // private void SyncKillCount()
    // {
    //     if (IsOwner)
    //     {
    //         Debug.Log($"Synced kill count: {killcount}");
    //     }
    // }
}
