using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    Vector2 moveDir = Vector2.zero;
    PlayerInput playerInput;
    bool interactPressed = false;
    bool backPressed = false;
    bool submitPressed = false;
    bool runPressed = false;
    bool pausePressed = false;
    

    public static InputManager instance { get; private set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("MORE THAN ONE INPUT MANAGER. PANIC!");
        }
        instance = this;
        playerInput = GetComponent<PlayerInput>();

    }


    public void MovePressed(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            moveDir = context.ReadValue<Vector2>();
        } 
        else if(context.canceled)
        {
            moveDir = context.ReadValue<Vector2>();
        }
        
    }

    public void RunPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            runPressed = true;
        } else if(context.canceled)
        {
            runPressed = false;
        }
    }

    public void InteractPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void BackPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            backPressed = true;
        }
        else if (context.canceled)
        {
            backPressed = false;
        }
    }

    public void PausePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausePressed = true;
        } else if(context.canceled)
        {
            pausePressed = false;
        }
    }


    public void RestartPressed(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(0);
    }


    //UI STUFF

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }



    public void SwitchActions(int map)
    {
        switch (map)
        {
            case 0:
                playerInput.SwitchCurrentActionMap("Main");
                break;
            case 1:
                playerInput.SwitchCurrentActionMap("UI");
                break;
            default:
                Debug.LogWarning("Warning: SwitchActions given invalid index");
                break;
        }


    }

    public Vector2 GetMoveDir()
    {
        return moveDir;
    } 

    public bool GetInteractPressed()
    {
        bool value = interactPressed;
        interactPressed = false;
        return value;
    }

    public bool GetSubmitPressed()
    {
        bool value = submitPressed;
        submitPressed = false;
        return value;
    }


    
    public bool GetRunPressed()
    {
        
        return runPressed;
    }

    public bool GetPausePressed()
    {
        bool value = pausePressed;
        pausePressed = false;
        return value;
    }

    public bool GetBackPressed()
    {
        bool value = backPressed;
        backPressed = false;
        return value;
    }

}
