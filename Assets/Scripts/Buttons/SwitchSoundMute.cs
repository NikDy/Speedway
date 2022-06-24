using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSoundMute : MonoBehaviour
{
    public void OnClick()
    {
        GameController.Instance.SwitchSoundMute();
        var data = GameController.Instance.localDataManager.ReadDataFromFile();
        data.isSoundMuted = GameController.Instance.soundSource.mute;
        GameController.Instance.localDataManager.WriteDataToFile(data);
    }
}
