using UnityEngine;

public class CharKey : KeyboardKey
{

    public string mainKey;
    public string modifierKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linkKeyboardInteract();
    }

    // Update is called once per frame

    public override void press()
    {
        if (keyboardInteract.shiftOn())
        {
            keyboardInteract.addChar(modifierKey);
        }
        else
        {
            keyboardInteract.addChar(mainKey);
        }
        flash();
    }
}
