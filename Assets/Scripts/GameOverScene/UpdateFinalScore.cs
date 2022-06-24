using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateFinalScore : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Text>().text = "Your Score: " + GameController.Instance.playerScore.ToString();
    }
}
