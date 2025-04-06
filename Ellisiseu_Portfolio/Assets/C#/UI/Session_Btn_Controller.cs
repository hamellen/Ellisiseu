using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Session_Btn_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI Name_Text;
    public TextMeshProUGUI Current_Text;
    public TextMeshProUGUI Max_Text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string name,string current,string max) {


        Name_Text.text = name;

        Current_Text.text = current;

        Max_Text.text = max;
    }
}
