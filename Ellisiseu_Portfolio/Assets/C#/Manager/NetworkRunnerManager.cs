using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using static Unity.Collections.Unicode;

public class NetworkRunnerManager : MonoBehaviour,INetworkRunnerCallbacks
{

   
    public NetworkRunner networkRunner;

    public List<SessionInfo> currentSessionList = new List<SessionInfo>();//세션 목록

    async void Start()
    {
        networkRunner = Manager.RESOURCES.Load<GameObject>("Prefab/fussion/NetworkRunner").GetComponent<NetworkRunner>();
        networkRunner.ProvideInput = false;

        await networkRunner.StartGame(new StartGameArgs()//로비모드
        {
            GameMode = GameMode.Client, // 세션에 바로 참가하지 않음
            SessionName = "", // 비워두면 "로비 대기 상태"
            SceneManager = FirebaseManager.GetNetworkSceneManager(),
            PlayerCount = 1
        });


    
    }

    public void RefreshSessionList()
    {

        GetCurrentSessionList().Forget();


        Debug.Log($"📥 세션 수신됨: {currentSessionList.Count}개");

        foreach (var session in currentSessionList)
        {
            Debug.Log($"세션 이름: {session.Name}, 인원: {session.PlayerCount}/{session.MaxPlayers}");
        }



    }

    async UniTaskVoid GetCurrentSessionList() {






    }



    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)//자동 호출
    {

        currentSessionList = sessionList;
        
    }



    public async void StartGame(GameMode mode, string roomname) {//host 와 client 사용


        networkRunner.AddCallbacks(this);

        var startGameArgs = new StartGameArgs()//세션 방설정
        {


            GameMode = mode,
            SessionName = roomname,
            IsVisible = true, 
            IsOpen = true,
            PlayerCount = 4,
            SceneManager = FirebaseManager.GetNetworkSceneManager(),

        };

        var result=await networkRunner.StartGame(startGameArgs);

        if (result.Ok) {
            Debug.Log("세션 생성  ");

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

    

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void Init() { 
    
    
        
    }

}
