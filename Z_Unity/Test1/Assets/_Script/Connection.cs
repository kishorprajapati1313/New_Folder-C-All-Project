using System;
using UnityEngine;
using FishNet;
using FishNet.Transporting;
using FishNet.Transporting.Tugboat;
using FishNet.Connection;
using FishNet.Managing.Client;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Connection : MonoBehaviour
{
    private Tugboat _tugboat;

    private void OnEnable() {
        InstanceFinder.ClientManager.OnClientConnectionState += OnClientConnectionState;
    }

    private void OnDisable() {
        InstanceFinder.ClientManager.OnClientConnectionState -= OnClientConnectionState;
    }

    private void OnClientConnectionState(ClientConnectionStateArgs args)
    {
        if(args.ConnectionState == LocalConnectionState.Stopping)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if(TryGetComponent(out Tugboat _t))
        {
            _tugboat = _t;
        }
        else
        {
            Debug.LogError("Could Not Connect", this);
            return;
        }

#if PARRELSYNC
        if(ParrelSync.ClonesManager.IsClone())
        {
            _tugboat.StartConnection(false);
        }
        else
        {
            _tugboat.StartConnection(true);
            _tugboat.StartConnection(false);
        }
#else
        _tugboat.StartConnection(true);
        _tugboat.StartConnection(false);
#endif
    }
}
