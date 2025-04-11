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

   
    public NetworkRunner Session_networkRunner;
    public NetworkRunner Game_networkRunner;

    public GameObject GO_Session;
    public GameObject GO_Game;


    public List<SessionInfo> currentSessionList = new List<SessionInfo>();//세션 목록


    private void Start()
    {
        
        EnterLobby().Forget(); 
    }

    async UniTaskVoid EnterLobby()
    {

        Debug.Log("EnterLobby 진입");

        if (Session_networkRunner != null)
        {
            if (Session_networkRunner.IsRunning)
                await Session_networkRunner.Shutdown();

            Destroy(Session_networkRunner.gameObject); // 💥 기존 runner 제거
        }

        // 새 runner 프리팹 인스턴스 생성
        GO_Session = Instantiate(Manager.RESOURCES.Load<GameObject>("Prefab/fussion/Session_Runner"));
        Session_networkRunner = GO_Session.GetComponent<NetworkRunner>();
        Session_networkRunner.ProvideInput = false;
        DontDestroyOnLoad(GO_Session);

        GO_Game = Instantiate(Manager.RESOURCES.Load<GameObject>("Prefab/fussion/Game_Runner"));
        Game_networkRunner = GO_Game.GetComponent<NetworkRunner>();
        Game_networkRunner.ProvideInput = false;
        DontDestroyOnLoad(GO_Game);


        await UniTask.Delay(100);

        try
        {

            Debug.Log("StartGame 시작");
            var result = await Session_networkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Host,
                SessionName = "",
                PlayerCount = 1,
                SceneManager = GO_Session.GetComponent<NetworkSceneManagerDefault>(),
                Scene = SceneManager.GetActiveScene().buildIndex
            });
            Debug.Log("StartGame 완료, 결과: " + result.ToString());

            Debug.Log("로비모드 진입");
        }
        catch (Exception ex)
        {
            Debug.LogError($"EnterLobby 예외 발생: {ex.Message}");
        }

       

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

        //if (Session_networkRunner.IsRunning)
        //{
        //    await Session_networkRunner.Shutdown();
        //}

        await Session_networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Host,
            SessionName = "",
            PlayerCount = 1,
            SceneManager = GO_Session.GetComponent<NetworkSceneManagerDefault>(),
            Scene = SceneManager.GetActiveScene().buildIndex
        });

    }



    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)//자동 호출
    {

        currentSessionList = sessionList;
        
    }



    public async void StartGame(GameMode mode, string roomname) {//host 와 client 사용


        Game_networkRunner.AddCallbacks(this);

        var startGameArgs = new StartGameArgs()//세션 방설정
        {


            GameMode = mode,
            SessionName = roomname,
            IsVisible = true, 
            IsOpen = true,
            PlayerCount = 4,
            SceneManager = FirebaseManager.GetNetworkRunnerManager().GO_Game.GetComponent<NetworkSceneManagerDefault>(),

        };

        var result=await Game_networkRunner.StartGame(startGameArgs);

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
