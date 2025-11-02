using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInteract : MonoBehaviour
{

    public TextAsset wordBankTextAsset;
    public AudioSource keySFX;
    public AudioSource stickyKeySFX;
    public AudioSource successSFX;
    public AudioSource failureSFX;
    public AudioSource bgm;
    public bool bgmFadeInP1 = false;
    public bool bgmFadeInP2 = false;
    public bool bgmFadeInP3 = false;
    public bool fiveBeepsP4 = false;
    public int fiveBeepsTimer = 0;
    public TextMeshPro displayText;

    GameObject keyboardBase;
    GameObject blankKeyboardBase;
    
    string currentInput = "";
    string chosenWord = "";
    string[] wordBank;

    int stickyKeysCounter = 0;
    float stickyKeysTimer = 0f;
    bool stickyKeysActive = false;

    int wordsCleared = 0;
    bool shiftActive = false;
    bool capsLockActive = false;

    float timeLeft = 360.0f;

    void Start()
    {
        string wordBankText = wordBankTextAsset.ToString();
        wordBank = wordBankText.Split("\n");
        chosenWord = wordBank[Random.Range(0, wordBank.Length)].Trim();

        keyboardBase = GameObject.Find("Base");
        blankKeyboardBase = GameObject.Find("BlankBase");
        blankKeyboardBase.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        displayString();
        stickyKeysUptime();
        bgmUpdate();
        fiveBeepsUpdate();

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            ScoreManager.setScore(wordsCleared);
            SceneManager.LoadScene("Score", LoadSceneMode.Single);
        }
    }

    public void toggleCapsLock()
    {
        playKeySFX();
        capsLockActive = !capsLockActive;
    }

    public bool capsLockOn()
    {
        return capsLockActive;
    }

    // called by the CharKey script, when a character key is clicked
    public void addChar(string letter)
    {
        playKeySFX();
        if (capsLockActive ^ shiftActive) // xor
        {
            letter = letter.ToUpper();
        }
        else
        {
            letter = letter.ToLower();
        }
        if (shiftActive && stickyKeysActive)
        {
            shiftActive = !shiftActive;
        }
        currentInput += letter;
        print("Current Input: " + currentInput);
    }

    // called by the Backspace script, when the backspace key is clicked
    public void backspace()
    {
        playKeySFX();
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            print("Current Input: " + currentInput);
        }
    }

    // called by the Shift script, when the shift key is clicked
    public void shift()
    {
        playKeySFX();
        if (stickyKeysActive)
        {
            shiftActive = !shiftActive;
        }
        stickyKeysCounter++;
        if (stickyKeysCounter == 5)
        {
            playStickyKeySFX();
            stickyKeysCounter = 0;
            stickyKeysActive = !stickyKeysActive;
        }
    }

    public bool shiftOn()
    {
        return shiftActive;
    }

    // you have to SPAM that shift button to toggle this stupid thing on (called in Update())
    public void stickyKeysUptime()
    {
        stickyKeysTimer += Time.deltaTime;
        if (stickyKeysTimer >= 2f)
        {
            stickyKeysCounter = 0;
            stickyKeysTimer = 0f;
        }
    }

    // called by the Enter script, when the enter key is clicked
    public void enter()
    {
        playKeySFX();
        if (currentInput != chosenWord)
        {
            playFailureSFX();
            currentInput = "";
        }
        else
        {
            nextWord();
        }
    }

    public void playKeySFX()
    {
        keySFX.pitch = Random.Range(0.8f, 1.2f); // pitch variation for, like, you know, not getting spammed with the same sfx every 0.01s
        keySFX.Play();
    }

    public void playStickyKeySFX()
    {
        if (shiftActive)
        {
            stickyKeySFX.pitch = 1.2f;
        }
        else
        {
            stickyKeySFX.pitch = 0.8f;
        }
        stickyKeySFX.Play();
    }

    public void playSuccessSFX()
    {
        successSFX.Play();
    }

    public void playFailureSFX()
    {
        failureSFX.Play();
    }

    public void bgmUpdate()
    {
        if (bgmFadeInP1 && bgm.volume < 0.25f)
        {
            bgm.volume += 0.0005f;
        }

        if (bgmFadeInP2 && bgm.volume < 0.5f)
        {
            bgm.volume += 0.0005f;
        }

        if (bgmFadeInP3 && bgm.volume < 1.0f)
        {
            bgm.volume += 0.002f;

        }
    }

    public void fiveBeepsUpdate()
    {
        if(fiveBeepsP4)
        {
            fiveBeepsTimer++;
            if (fiveBeepsTimer % 20 == 0)
            {
                stickyKeySFX.pitch = 2.0f;
                stickyKeySFX.Play();
            }
            if (fiveBeepsTimer > 100)
            {
                fiveBeepsP4 = false;
                fiveBeepsTimer = 0;
            }
        }
    }

    public void bgmActivateP1()
    {
        bgmFadeInP1 = true;
        bgm.volume = 0.0f;
        bgm.Play();
        bgm.loop = true;
    }

    public void bgmActivateP2()
    {
        bgmFadeInP2 = true;
        bgm.volume = 0.25f;
    }

    public void bgmActivateP3()
    {
        bgmFadeInP3 = true;
        bgm.volume = 0.5f;
    }

    public void bgmActivateP4()
    {
        fiveBeepsP4 = true;
    }

    // https://simple.wikipedia.org/wiki/Leet
    protected void abysmalSpeakVowels()
    {

        chosenWord = chosenWord.Replace("a", "@").Replace("A", "^");
        chosenWord = chosenWord.Replace("e", "3").Replace("E", "3");
        chosenWord = chosenWord.Replace("i", "!").Replace("I", "!");
        chosenWord = chosenWord.Replace("o", "0").Replace("O", "()");
        chosenWord = chosenWord.Replace("u", "u").Replace("U", "[_]");
        chosenWord = chosenWord.Replace("y", "'/").Replace("Y", "\\|/");
    }

    protected void abysmalSpeakConsonants()
    {
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("b", "6").Replace("B", "|3");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("c", "<").Replace("C", "<");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("d", "o|").Replace("D", "|)");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("f", "{=").Replace("F", "{=");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("g", "-").Replace("G", "{+");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("h", "#").Replace("H", "#");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("j", "_!").Replace("J", "_]");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("k", "|<").Replace("K", "|<");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("l", "1").Replace("L", "1");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("m", "^^").Replace("M", @"|\/|");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("n", @"/V").Replace("N", @"/V");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("p", "|o").Replace("P", "|*");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("q", "9").Replace("Q", "O_");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("r", "{^").Replace("R", "|2");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("s", "$").Replace("S", "$");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("t", "+").Replace("T", "+");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("v", @"\/").Replace("V", @"\/");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("w", @"uu").Replace("W", @"UU");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("x", "><").Replace("X", "}{");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("y", "'/").Replace("Y", "\\|/");
        if (Random.value < 0.5) chosenWord = chosenWord.Replace("z", "2").Replace("Z", "2");

    }

    public void nextWord()
    {
        playSuccessSFX();
        chosenWord = wordBank[Random.Range(0, wordBank.Length)].Trim();
        wordsCleared++;

        if (wordsCleared >= 5)
        {
            abysmalSpeakVowels();
            if (wordsCleared == 5) bgmActivateP1();
        }

        if (wordsCleared >= 8)
        {
            abysmalSpeakConsonants();
            if (wordsCleared == 8) bgmActivateP2();
        }

        if (wordsCleared == 11)
        {
            bgmActivateP3();
            // hide keyboard keys
            keyboardBase.GetComponent<Renderer>().enabled = false;
            blankKeyboardBase.GetComponent<Renderer>().enabled = true;
        }

        if (wordsCleared == 15)
        {
            bgmActivateP4();
        }
        print(wordsCleared);
        currentInput = "";
    }
    
    public void displayString()
    {
        string playerInput = currentInput;

        if (wordsCleared >= 15) // last phase, hide input
        {
            playerInput = Regex.Replace(playerInput, ".", "*");
        }
        string displayString = chosenWord + "\n" + playerInput;
        displayText.text = displayString;
    }
}