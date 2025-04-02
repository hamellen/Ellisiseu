using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRunnerManager : MonoBehaviour,INetworkRunnerCallbacks
{

   
    public NetworkRunner networkRunner;

    void Start()
    {
        networkRunner = Manager.RESOURCES.Load<GameObject>("Prefab/fussion/NetworkRunner").GetComponent<NetworkRunner>();
    }

    public async void StartGame(GameMode mode, string roomname) {


        networkRunner.AddCallbacks(this);

        var startGameArgs = new StartGameArgs()//技记 规汲沥
        {


            GameMode = mode,
            SessionName = roomname,
            PlayerCount = 4,
            SceneManager = FirebaseManager.GetNetworkSceneManager(),

        };

        var result=await networkRunner.StartGame(startGameArgs);

        if (result.Ok) {
            Debug.Log("技记 积己  ");

            string Scene_name = "GameStage";
            networkRunner.SetActiveScene(Scene_name);
        }
    }



    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
       
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void Init() { 
    
    
        
    }

}
