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

        var buildIndex = SceneManager.GetSceneByName("GameStage").buildIndex;

        var startGameArgs = new StartGameArgs()//세션을 만든 유저가 자동으로 해당 씬으로 이동
        {
            GameMode = GameMode.Host,
            SessionName = inputField.text,
            PlayerCount = 5,
            Scene = buildIndex,
            SceneManager = FirebaseManager.GetNetworkRunnerManager().GO_Game.GetComponent<NetworkSceneManagerDefault>()
        };

        try
        {
            
            Debug.Log("StartGame 시작");
            await runner.StartGame(startGameArgs);
            Debug.Log("StartGame 완료");

            Debug.Log("로비모드 진입");
        }
        catch (Exception ex)
        {
            Debug.LogError($"EnterLobby 예외 발생: {ex.Message}");
        }
    }

    public void CloseTab() {

        Manager.UI.ClosePopUp();
    }
}
