using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.Net.WebRequestMethods;

public class Movement2D : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]PlayerInput player_input;
    [SerializeField] Vector2 moveDirection2d,Direction2d;

    [SerializeField]Animator animator;
    [SerializeField] Rigidbody2D rigid;

    public float movespeed = 5.0f;


    private void Awake()
    {
        player_input = new PlayerInput();

        player_input.Player.Movement.started += ActiveMovement;
        player_input.Player.Movement.performed += ActiveMovement;
        player_input.Player.Movement.canceled += ActiveMovementEnd;

        player_input.Player.Direction.started += ActiveDirection;
        player_input.Player.Direction.performed += ActiveDirection;
    }

    private void OnEnable()
    {
        player_input.Player.Movement.Enable();
        player_input.Player.Direction.Enable();
    }

    void Start()
    {

        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

    }

    public void ActiveMovementEnd(InputAction.CallbackContext value)
    {

        moveDirection2d = new Vector2(0, 0);
        animator.SetBool("IsMove", false);
    }

    public void ActiveMovement(InputAction.CallbackContext value)
    {

        moveDirection2d = value.ReadValue<Vector2>();
        animator.SetBool("IsMove", true);

        
    }

    public void ActiveDirection(InputAction.CallbackContext value) {

        Direction2d = value.ReadValue<Vector2>();

        if (Direction2d.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Direction2d.x < 0) {

            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("aimDirection", (float)(Direction2d.y - 0.5) * 180f);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.MovePosition(rigid.position + moveDirection2d * movespeed * Time.fixedDeltaTime);

    }
}
