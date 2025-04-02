using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_UI_Controller : MonoBehaviour
{

    public Action Weapon_fire;
    public Action Weapon_reload;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reload() {

        Weapon_reload();
    }

    public void Fire() {


        Weapon_fire();
    }
}
