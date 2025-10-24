using UnityEngine;

public class CapsLock : KeyboardKey
{

    public string mainKey;
    public string modifierKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linkKeyboardInteract();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void press()
    {
        print("Caps Lock On.");
    }
}
