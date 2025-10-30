using UnityEngine;

public class Backspace : KeyboardKey
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linkKeyboardInteract();

    }

    // Update is called once per frame

    public override void press()
    {
        keyboardInteract.backspace();
        flash();
    }
}
