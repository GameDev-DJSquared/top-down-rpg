using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    //Stuff to edit in Editor (not final values)
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;

    static bool canMove = true;
    Rigidbody2D rb;
    Vector2 moveDir = Vector2.zero;
    bool running = false;

    public delegate void PlayerMoveHandler(Vector2 pos);
    public event PlayerMoveHandler MoveEvent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //Get Variables from Input
        moveDir = InputManager.instance.GetMoveDir();
        running = InputManager.instance.GetRunPressed();

        if (MoveEvent != null) MoveEvent.Invoke(moveDir);

        if (moveDir != Vector2.zero && canMove)
        {

            rb.MovePosition(rb.position + moveDir * (running ? runSpeed : walkSpeed) * Time.fixedDeltaTime);
        }
    }

    public static void SetCanMove(bool value)
    {
        canMove = value;
    }


    public static bool GetCanMove()
    {
        return canMove;
    }
}
