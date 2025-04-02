using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool IsPossess = false;
    public Animator animator;

    Movement2D movement;
    // Start is called before the first frame update
    void Start()
    {
        Manager.GetManager();
        FirebaseManager.GetFireBaseManager();

        animator = GetComponent<Animator>();

        movement = GetComponent<Movement2D>();
        FindObjectOfType<Basic_UI_Controller>().Weapon_fire+= Weapon_Fire;
        FindObjectOfType<Basic_UI_Controller>().Weapon_reload += Weapon_Reload;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Possess_Interaction() {

        if (!IsPossess)
        {
            
           IsPossess = !IsPossess;
           movement.enabled = IsPossess;
        }
        else if (IsPossess) {

            IsPossess = !IsPossess;
            movement.enabled = IsPossess;

        }
    
    }

    public void Weapon_Fire() {

        animator.SetTrigger("Fire");
    }

    public void Weapon_Reload() {

        animator.SetTrigger("Reload");
    }
}
