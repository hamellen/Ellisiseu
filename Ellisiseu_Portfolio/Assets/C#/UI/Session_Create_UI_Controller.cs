using Cysharp.Threading.Tasks;
using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.Unicode;


public class Session_Create_UI_Controller : MonoBehaviour
{

    [SerializeField] TMP_InputField inputField;


    public void CreateSession() {

        Debug.Log("🟢 CreateSession 호출됨");
        EnterSession().Forget();
    }


    public async UniTaskVoid EnterSession() {

        var runner = FirebaseManager.GetNetworkRunnerManager().Game_networkRunner;

        if (runner.IsRunning)
        {
            Debug.Log("기존 Runner 세션 종료 시도");
            await runner.Shutdown();

            // ✅ 확실히 기다려주는 코드
            while (runner.IsRunning)
            {
               
                await UniTask.DelayFrame(1);
            }
        }

        await PrepareSceneBeforeFusionStart();


        var startGameArgs = new StartGameArgs()//세션을 만든 유저가 자동으로 해당 씬으로 이동
        {
            GameMode = GameMode.Host,
            SessionName = inputField.text,
            PlayerCount = 5,
            SceneManager = FirebaseManager.GetNetworkRunnerManager().GO_Game.GetComponent<NetworkSceneManagerDefault>()
        };

        var result=await runner.StartGame(startGameArgs);

        if (result.Ok) {


            const string scene_name = "GameStage";
            runner.SetActiveScene(scene_name);
        }
    }

    private async UniTask PrepareSceneBeforeFusionStart()
    {
        // 현재 씬 이름 가져오기
        Scene currentScene = SceneManager.GetActiveScene();

        // 씬 언로드 시도
        if (currentScene.name == "GameLobby")
        {
            await SceneManager.UnloadSceneAsync(currentScene.name);
        }

       
    }


    public void CloseTab() {

        Manager.UI.ClosePopUp();
    }
}
