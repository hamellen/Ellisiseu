using Cysharp.Threading.Tasks;
using Fusion;
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


        EnterSession().Forget();
    }


    public async UniTask EnterSession() {

        var runner = FirebaseManager.GetNetworkRunnerManager().networkRunner;

        if (runner.IsRunning)
        {
            Debug.Log("기존 Runner 세션 종료 시도");
            await runner.Shutdown();
        }

        var buildIndex = SceneManager.GetSceneByName("GameStage").buildIndex;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = inputField.text,
            PlayerCount = 5,
            Scene = buildIndex,
            SceneManager = FirebaseManager.networkSceneManager
        };

        var result = await runner.StartGame(startGameArgs);

        if (result.Ok)
        {
            Debug.Log("세션 시작 성공, 씬 직접 로드");
        }
        else
        {
            Debug.LogError($"세션 시작 실패: {result.ShutdownReason}");
        }
    }

    public void CloseTab() {

        Manager.UI.ClosePopUp();
    }
}
