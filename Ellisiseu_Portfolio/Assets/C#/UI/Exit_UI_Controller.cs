using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_UI_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Exit_Game() {


        Application.Quit();

        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Close_Exit() {


        Manager.UI.ClosePopUp();
    }

}
