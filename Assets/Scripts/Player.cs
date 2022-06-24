using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerPositions;
    public GameObject trafficController;
    public float blinkPeriod = 1f;
    public GameObject gameHandler;

    private List<Transform> playerPositionsTransformsList;
    private int currentPlayerPositionInList;
    private bool playerInSafeSate;
    private float safeStateBeginTime;
    private float lastRenderModeSwitchTime;
    private StandardShaderUtils.BlendMode currentBlendMode;
    private float gameRunningTime;

    // Start is called before the first frame update
    void Start()
    {
        currentBlendMode = StandardShaderUtils.BlendMode.Opaque;
        playerInSafeSate = false;
        playerPositionsTransformsList = LoadPlayerPositionsList(playerPositions);
        currentPlayerPositionInList = 0;
        gameObject.transform.position = playerPositionsTransformsList[0].position;
        gameHandler = GameController.Instance.gameObject;
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.gameRunning)
        {
            PlayerModelBlink();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.gameRunning)
        {
            gameRunningTime = Time.time;
        }
    }


    List<Transform> LoadPlayerPositionsList(GameObject playerPositions)
    {
        List<Transform> playerPositionList = new List<Transform>();
        foreach (Transform child in playerPositions.transform)
        {
            playerPositionList.Add(child);
        }
        playerPositionList.Sort(delegate (Transform first, Transform second)
        {
            if (first.position.x < second.position.x) return 1;
            else return 0;
        });
        return playerPositionList;
    }


    public void MoveLeft()
    {
        if (currentPlayerPositionInList - 1 >= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("TurnLeft");
        }
    }

    public void MovePlayerColliderLeft()
    {
        transform.position = playerPositionsTransformsList[currentPlayerPositionInList - 1].position;
        currentPlayerPositionInList--;
        //gameObject.GetComponent<Animator>().ResetTrigger("TurnLeft");
    }



    public void MoveRight()
    {
        if (currentPlayerPositionInList + 1 < playerPositionsTransformsList.Count)
        {
            
            gameObject.GetComponent<Animator>().SetTrigger("TurnRight");
        }
    }


    public void MovePlayerColliderRight()
    {
        transform.position = playerPositionsTransformsList[currentPlayerPositionInList + 1].position;
        currentPlayerPositionInList++;
        //gameObject.GetComponent<Animator>().ResetTrigger("TurnRight");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traffic") && !playerInSafeSate)
        {
            trafficController.GetComponent<TrafficController>().PlayerTriggerDetect(other);
            PlayerHit();
            playerInSafeSate = true;
            safeStateBeginTime = Time.time;
            lastRenderModeSwitchTime = Time.time;
        }
    }

    
    private void PlayerHit()
    {
        GameController.Instance.soundSource.PlayOneShot(GameController.Instance.carBreak);
        GameController.Instance.UpdatePlayerLiveCount(GameController.Instance.GetPlayerLiveCount() - 1);
        if (!IsPlayerStillHaveLives()) GameController.Instance.GameOver();
    }


    private bool IsPlayerStillHaveLives()
    {
        return GameController.Instance.GetPlayerLiveCount() > 0;
    }


    private void PlayerModelBlink()
    {
        if (playerInSafeSate &&
            Time.time - safeStateBeginTime < GameController.Instance.playerSafeTime &&
            GameController.Instance.gameRunning)
        {
            gameObject.GetComponent<Animator>().SetBool("Blinking", true);
        }
        else if (GameController.Instance.gameRunning)
        {
            gameObject.GetComponent<Animator>().SetBool("Blinking", false);
            playerInSafeSate = false;
        }
    }

}
