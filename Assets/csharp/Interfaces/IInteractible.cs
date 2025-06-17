using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public interface IInteractible
{
    /// <summary>
    /// The interactible is made ready to interact.
    /// </summary>
    public void Activate();

    /// <summary>
    /// The interactible is made non-interactible.
    /// </summary>
    public void Deactivate();

    /// <summary>
    /// The interactible is interacted with.
    /// </summary>
    public void Interact(CallbackContext ctx);
}
