using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficBehaviour : MonoBehaviour
{

    public float moveSpeed = 1;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.gameRunning)
        {
            transform.Translate(Vector3.forward * moveSpeed);
        }
    }
}
