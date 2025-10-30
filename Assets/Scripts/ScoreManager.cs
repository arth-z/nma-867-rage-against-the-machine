using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;
    public static int finalScore = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void setScore(int score)
    {
        finalScore = score;
    }

    public static int getScore()
    {
        return finalScore;
    }
    
}
