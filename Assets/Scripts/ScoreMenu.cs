using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class ScoreMenu : MonoBehaviour
{
    InputAction enterAction;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        enterAction = new InputAction("Enter", binding: "<Keyboard>/enter");
        enterAction.performed += ctx => Entered();
        enterAction.Enable();
    }

    void Update()
    {
        scoreText.text = "Words cleared: " + ScoreManager.getScore().ToString();
    }
    
    void Entered()
    {
        ScoreManager.setScore(0);
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
