using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobby : MonoBehaviour
{

    public AudioClip MainBGM;

    // Start is called before the first frame update
    void Start()
    {
        Manager.GetManager();
        FirebaseManager.GetFireBaseManager();

        Manager.SOUNDMANAGER.Play(Define.Sound.Bgm, MainBGM, 1.0f);
    }

    public void Popup_Exit() {


        Manager.UI.ShowPopUI("PopUp_Exit");
    }

    public void ShowSessionCount() {

        Manager.UI.ShowPopUI("PopUp_Session");
        FirebaseManager.GetNetworkRunnerManager().RefreshSessionList();

    }

    public void ShowSettingUI() {

        Manager.UI.ShowPopUI("PopUp_Setting");
    }
    
}
