using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

using static Unity.Collections.Unicode;
using UnityEngine.SceneManagement;

public class NetworkRunnerManager : MonoBehaviour,INetworkRunnerCallbacks
{

   
    public NetworkRunner networkRunner;

    public List<SessionInfo> currentSessionList = new List<SessionInfo>();//세션 목록

    

    async void Start()
    {
        if (networkRunner != null)
        {
            if (networkRunner.IsRunning)
                await networkRunner.Shutdown();

            Destroy(networkRunner.gameObject); // 💥 기존 runner 제거
        }

        // 새 runner 프리팹 인스턴스 생성
        var runnerGO = Instantiate(Manager.RESOURCES.Load<GameObject>("Prefab/fussion/NetworkRunner"));
        networkRunner = runnerGO.GetComponent<NetworkRunner>();
        networkRunner.ProvideInput = false;

        await networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Client,
            SessionName = "",
         
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

    async UniTask GetCurrentSessionList() {

        if (networkRunner.IsRunning)
        {
            await networkRunner.Shutdown();
        }

        await networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Client,
            SessionName = "",

            SceneManager = FirebaseManager.GetNetworkSceneManager(),
            PlayerCount = 1
        });

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
            //await networkRunner.SetActiveScene(Scene_name);
        }
    }

    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
   
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
   
    
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
   

    

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void Init() { 
    
    
        
    }

   

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }


}
