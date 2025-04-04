using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager 
{
    int order = 0;

    Stack<UI_POPUP> popupStack = new Stack<UI_POPUP>();

   

    public void  ShowPopUI(string name)
    {
        order++;
        GameObject go = Manager.RESOURCES.Instantiate($"UI/PopUp/{name}");

        go.GetComponent<Canvas>().sortingOrder = order;
        popupStack.Push(go.GetComponent<UI_POPUP>());

       
    }

    public void ShowBasicUI(string name)
    {

        Manager.RESOURCES.Instantiate($"UI/{name}");


    }

    

    public void ClosePopUp()
    {
        order--;
        if (popupStack.Count == 0)
        {
            return;
        }
        else if (popupStack.Count > 0)
        {

            UI_POPUP popup = popupStack.Pop();

            Manager.RESOURCES.Destroy(popup.gameObject);
        }
    }

    public void CloseAllPopUp() {

        foreach (UI_POPUP popup in popupStack) {

            Manager.RESOURCES.Destroy(popup.gameObject);
        }
    }

}
