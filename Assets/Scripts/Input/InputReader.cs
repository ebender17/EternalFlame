using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public enum GameSchemas { Gameplay = 0, Menus, Dialogue, None }

/// <summary>
/// Made a scriptable object so it can be acessed from anywhere in project. 
/// </summary>
[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    //private PlayerInputs playerInput;

    private GameInput gameInput;

    public GameSchemas CurrentSchema;


    //Assign deletgate{} to events to initialize them with an empty delegate
    // so we can skip the null check when we use them 

    //Gameplay
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction interactEvent = delegate { };
    public event UnityAction attackEvent = delegate { };
    public event UnityAction attackCanceledEvent = delegate { };

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Gameplay.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            interactEvent.Invoke();
        }
        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            attackEvent.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            attackCanceledEvent.Invoke();
        }
    }

    public void EnableGameplayInput()
    {
        gameInput.Gameplay.Enable();

        CurrentSchema = GameSchemas.Gameplay;
    }

    public void DisableAllInput()
    {
        gameInput.Gameplay.Disable();

        CurrentSchema = GameSchemas.None;
    }
}
