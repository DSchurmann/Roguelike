using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    //Gameplay Movement
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private bool run;
    private bool diagonal;

    //actions
    private bool attack;
    private bool menu;

    //movement
    public void GetUp(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            up = true;
        }
        if(ctx.canceled)
        {
            up = false;
        }
    }
    public bool SetUpFalse() => up = false;
    public bool Up
    {
        get => up;
    }

    public void GetDown(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            down = true;
        }
        if (ctx.canceled)
        {
           down = false;
        }
    }
    public bool SetDownFalse() => down = false;
    public bool Down
    {
        get => down;
    }

    public void GetLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            left = true;
        }
        if (ctx.canceled)
        {
            left = false;
        }
    }
    public bool SetLeftFalse() => left  = false;
    public bool Left
    {
        get => left;
    }

    public void GetRight(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            right = true;
        }
        if (ctx.canceled)
        {
            right = false;
        }
    }
    public bool SetRightFalse() => right = false;
    public bool Right
    {
        get => right;
    }

    public void GetRun(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            run = true;
        }
        if (ctx.canceled)
        {
            run = false;
        }
    }
    public bool SetRunFalse() => run = false;
    public bool Run
    {
        get => run;
    }

    public void GetDiagonal(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            diagonal = true;
        }
        if (ctx.canceled)
        {
            diagonal = false;
        }
    }
    public bool SetDiagonalFalse() => diagonal = false;
    public bool Diagonal
    {
        get => diagonal;
    }

    //attack input
    public void GetAttack(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            attack = true;
        }
        if(ctx.canceled)
        {
            attack = false;
        }
    }
    public void SetAttackFalse() => attack = false;
    public bool Attack
    {
        get => attack;
    }
    
    //menu
    public void GetMenu(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            menu = true;
        }
        if(ctx.canceled)
        {
            menu = false;
        }
    }
    public void SetMenuFalse() => menu = false;
    public bool Menu
    {
        get => menu;
    }
}
