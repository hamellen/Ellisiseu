using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Session_UI_Controller : MonoBehaviour
{

    

    public int order;

    public Transform Context;

    public List<SessionInfo> currentSessionList=new List<SessionInfo>();
    // Start is called before the first frame update
    void Start()
    {
        order = GetComponent<Canvas>().sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh_Click() {

        

        Session_Btn_Controller[] session_Btn_Controllers = Context.GetComponentsInChildren<Session_Btn_Controller>();


        foreach (var btn in session_Btn_Controllers) { //√ ±‚»≠

            Destroy(btn.gameObject);
        }

        FirebaseManager.GetNetworkRunnerManager().RefreshSessionList();

        foreach (var session in FirebaseManager.GetNetworkRunnerManager().currentSessionList) {

            GameObject go = Manager.UI.ShowPopUI("Session_Btn");
            go.transform.SetParent(Context);
            go.GetComponent<Session_Btn_Controller>().SetText(session.Name,session.PlayerCount.ToString(),session.MaxPlayers.ToString());
        }
    }


    public void Close_PopUp() {

        Manager.UI.ClosePopUp();
    }

    public void Add_Session() {


        Manager.UI.ShowPopUI("PopUp_Session_Create");
    }
}
