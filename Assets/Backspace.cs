using UnityEngine;

public class Backspace : KeyboardKey
{

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
        print("Backspaced.");
    }
}
