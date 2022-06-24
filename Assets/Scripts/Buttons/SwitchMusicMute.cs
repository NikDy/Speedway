using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicMute : MonoBehaviour
{
    public void OnClick()
    {
        GameController.Instance.SwitchMusicMute();
        var data = GameController.Instance.localDataManager.ReadDataFromFile();
        data.isMusicMuted = GameController.Instance.musicSource.mute;
        GameController.Instance.localDataManager.WriteDataToFile(data);
    }    
}
