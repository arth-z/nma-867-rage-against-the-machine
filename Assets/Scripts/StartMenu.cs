using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    InputAction enterAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enterAction = new InputAction("Enter", binding: "<Keyboard>/enter");
        enterAction.performed += ctx => Entered();
        enterAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void Entered()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
