using UnityEngine;
using UnityEngine.InputSystem;

public class Shift : KeyboardKey
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linkKeyboardInteract();
    }

    public override void press()
    {
        keyboardInteract.shift();
        flash();
    }
}
