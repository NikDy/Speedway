using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOverButton : MonoBehaviour
{
    public void StartOver()
    {
        GameController.Instance.StartOver();
    }
}
