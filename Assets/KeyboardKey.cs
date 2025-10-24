using UnityEngine;

public abstract class KeyboardKey : MonoBehaviour
{

    // helpful for all child classes
    KeyboardInteract keyboardInteract;
    public void linkKeyboardInteract()
    {
        keyboardInteract = transform.GetComponentInParent<KeyboardInteract>();
    }

    // called when the keyboard key is pressed
    public abstract void press();

}
