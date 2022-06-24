using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPause : MonoBehaviour
{
    public void OnClick()
    {
        if (GameController.Instance.gameRunning)
        {
            GameController.Instance.PauseGame();
            GameController.Instance.GetComponent<GUIController>().ChangePauseMenuVisability();
        }
        else
        {
            GameController.Instance.UnpauseGame();
            GameController.Instance.GetComponent<GUIController>().ChangePauseMenuVisability();
        }
        
    }
}
