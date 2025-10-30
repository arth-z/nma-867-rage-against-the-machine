using UnityEngine;

public abstract class KeyboardKey : MonoBehaviour
{

    // helpful for all child classes
    protected KeyboardInteract keyboardInteract;
    protected int flashDuration = 6;
    protected int flashTimer = 0;
    protected void linkKeyboardInteract()
    {
        keyboardInteract = transform.GetComponentInParent<KeyboardInteract>();
    }

    protected void Update()
    {
        flashingUpdate();
    }

    // called when the keyboard key is pressed
    public abstract void press();

    public void flash()
    {
        flashTimer = flashDuration;
    }

    public void flashingUpdate()
    {
        if (flashTimer > 0)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            flashTimer--;
        }
        else if (flashTimer == 0)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

}
