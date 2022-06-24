using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{

    public float scrollSpeedScale = 0.1f;


    private Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.gameRunning)
        {
            MoveUV();
        }
    }


    void MoveUV()
    {
        float offset = Time.time * GameController.Instance.currentGameSpeed * scrollSpeedScale;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset - (int)offset));
    }

}
