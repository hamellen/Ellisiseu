using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Setting_UI_Controller : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI email_text;
    [SerializeField] Slider BGM_Slider;
    [SerializeField] Slider SFX_Slider;

    private void Start()
    {

        //email_text.text = FirebaseManager.GetFireBaseManager().user.Email;
        BGM_Slider.value = Manager.SOUNDMANAGER.BGM_value;
        SFX_Slider.value = Manager.SOUNDMANAGER.SFX_value;

        BGM_Slider.onValueChanged.AddListener(SetBgmValue);
        SFX_Slider.onValueChanged.AddListener(SetSfxValue);
    }

    public void CloseTab() {


        Manager.UI.ClosePopUp();
    }

    public void SetBgmValue(float value) {

        Manager.SOUNDMANAGER.Change_Sound_Value(Define.Sound.Bgm, value);
    }

    public void SetSfxValue(float value) {


        Manager.SOUNDMANAGER.Change_Sound_Value(Define.Sound.D2_Effect, value);
    }
}

