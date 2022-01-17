using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBackGround_SubwayMinigame2 : MonoBehaviour
{
    private Vector3 startPos;
    public float speedBG = 5;

    private void Start()
    {
        startPos = transform.position;
    }


    private void Update()
    {
        if(!GameController_SubwayMinigame2.instance.isWin && !GameController_SubwayMinigame2.instance.isLose && !GameController_SubwayMinigame2.instance.isStop)
        {
            transform.Translate(Vector3.left * speedBG * Time.deltaTime);
            if (transform.position.x < -13.13f)
            {
                transform.position = startPos;
            }
        }
    }
}
