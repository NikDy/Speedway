using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{

    public List<Text> textLabelsDict;
    public List<Button> buttonsDict;
    public CanvasGroup pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        textLabelsDict = new List<Text>();
        foreach (Text t in FindObjectsOfType<Text>())
        {
            textLabelsDict.Add(t);
        }
        buttonsDict = new List<Button>();
        foreach (Button t in FindObjectsOfType<Button>())
        {
            buttonsDict.Add(t);
        }
        foreach (CanvasGroup g in FindObjectsOfType<CanvasGroup>())
        {
            if (g.name == "Pause Menu") pauseMenu = g;
        }
    }

    public void LockControlButtons()
    {
        buttonsDict.Find(i => i.name == "Move Right Button").enabled = false;
        buttonsDict.Find(i => i.name == "Move Left Button").enabled = false;
    }

    public void UnlockControlButtons()
    {
        buttonsDict.Find(i => i.name == "Move Right Button").enabled = true;
        buttonsDict.Find(i => i.name == "Move Left Button").enabled = true;
    }


    public void ChangePauseMenuVisability()
    {
        if (pauseMenu.alpha == 1f)
        {
            pauseMenu.alpha = 0f;
            pauseMenu.blocksRaycasts = false;
        }
        else
        {
            pauseMenu.alpha = 1f;
            pauseMenu.blocksRaycasts = true;
        }
    }


    public bool TryChangeTextValue(string textName, string newValue)
    {
        foreach (Text text in textLabelsDict)
        {
            if (text.name == textName)
            {
                text.text = newValue;
                return true;
            }
        }
        return false;
    }

}
