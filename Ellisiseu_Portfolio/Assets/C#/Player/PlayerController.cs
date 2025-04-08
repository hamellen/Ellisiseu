using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool IsPossess = false;
    public Animator animator;

    [SerializeField] Transform Fire_Trans;
    [SerializeField] List<AudioClip> fire_clips;
    [SerializeField] GameObject pre_Bullet;

    Movement2D movement;
    // Start is called before the first frame update
    void Start()
    {

        Manager.UI.ShowBasicUI("Basic_UI");

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
        Manager.SOUNDMANAGER.Play(Define.Sound.D2_Effect, fire_clips[Random.Range(0, fire_clips.Count)], 1.0f);
        //Manager.RESOURCES.Instantiate("Player/Bullet", Fire_Trans);
        Instantiate(pre_Bullet, Fire_Trans.position, Fire_Trans.rotation);
    }

    public void Weapon_Reload() {

        animator.SetTrigger("Reload");
    }
}
