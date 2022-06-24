using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficController : MonoBehaviour
{

    public GameObject trafficSpawnPositions;
    public GameObject trafficExitCollider;
    public List<GameObject> trafficSpawnableObjects;
    public float trafficSpawnPeriodDelay;
    public int trafficMaxNumber;
    public float trafficBasicSpeed;
    public float trafficDeltaSpeed;
    public float gameSpeedScale;

    private List<Transform> trafficPositionsTransformsList;
    private int trafficCurrentNumber = 0;
    private float trafficLastSpawnTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        trafficPositionsTransformsList = LoadTrafficSpwanPositionsList(trafficSpawnPositions);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.gameRunning)
        {
            TrySpawnNewTraffic();
        }
    }


    void TrySpawnNewTraffic()
    {
        if (trafficCurrentNumber < trafficMaxNumber &&
            Time.time - trafficLastSpawnTime > trafficSpawnPeriodDelay)
        {           
            var newTraffic = Instantiate(trafficSpawnableObjects[Random.Range(0, trafficSpawnableObjects.Count)],
                                         trafficPositionsTransformsList[Random.Range(0, trafficPositionsTransformsList.Count)].position,
                                         Quaternion.identity);
            newTraffic.GetComponent<TrafficBehaviour>().moveSpeed = trafficBasicSpeed + 
                                                                    GameController.Instance.currentGameSpeed * gameSpeedScale +
                                                                    Random.Range(-trafficDeltaSpeed, trafficDeltaSpeed);
            trafficLastSpawnTime = Time.time;
            trafficCurrentNumber++;
        }
    }


    List<Transform> LoadTrafficSpwanPositionsList(GameObject trafficSpawnPositions)
    {
        List<Transform> trafficSpawnPositionsList = new List<Transform>();
        foreach (Transform child in trafficSpawnPositions.transform)
        {
            trafficSpawnPositionsList.Add(child);
        }
        trafficSpawnPositionsList.Sort(delegate (Transform first, Transform second)
        {
            if (first.position.x < second.position.x) return 1;
            else return 0;
        });
        return trafficSpawnPositionsList;
    }


    public void ExitTriggerDetect(Collider collider)
    {
        Destroy(collider.gameObject);
        trafficCurrentNumber--;
    }


    public void PlayerTriggerDetect(Collider collider)
    {
        Destroy(collider.gameObject);
        trafficCurrentNumber--;
    }
}
