using UnityEngine;
using UnityEngine.InputSystem;

public class MenusInputManager : MonoBehaviour
{

    private Vector2 navRaw;
    private float x;
    private float y;

    private bool accept;
    private bool decline;

    public void GetNav(InputAction.CallbackContext ctx)
    {
        navRaw = ctx.ReadValue<Vector2>();
        x = (navRaw * Vector2.right).normalized.x;
        y = (navRaw * Vector2.up).normalized.y;
    }
    public float X
    {
        get => x;
    }
    public float Y
    {
        get => y;
    }


    public void GetAccept(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            accept = true;
        }
        if(ctx.canceled)
        {
            accept = false;
        }
    }
    public void SetAcceptFalse() => accept = false;
    public bool Accept
    {
        get => accept;
    }
    
    public void GetDecline(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            decline = true;
        }
        if(ctx.canceled)
        {
            decline = false;
        }
    }
    public void SetDeclineFalse() => decline = false;
    public bool Decline
    {
        get => decline;
    }
}