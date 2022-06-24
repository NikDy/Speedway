using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBestScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalSaveData data = GameController.Instance.localDataManager.ReadDataFromFile();
        if (data != null)
        {
            if (data.maxScore < GameController.Instance.playerScore) data.maxScore = GameController.Instance.playerScore;
        }
        else
        {
            data = new LocalSaveData();
            data.maxScore = GameController.Instance.playerScore;
        }
        GameController.Instance.localDataManager.WriteDataToFile(data);

        gameObject.GetComponent<Text>().text = "Best Score: " + data.maxScore;
    }

}
