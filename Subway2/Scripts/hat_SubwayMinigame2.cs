using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hat_SubwayMinigame2 : MonoBehaviour
{
    public int myNumber;
    public SpriteRenderer spriteNumber;
    public float speed = 1.5f;



    void HandleOnChangeSpeedBG(float speedBG)
    {
        if (speedBG == 6)
        {
            speed = 2.5f;
        }
        else if(speedBG == 7)
        {
            speed = 3.5f;
        }
        else if(speedBG == 8)
        {
            speed = 4.5f;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-7.5f, transform.position.y), speed * Time.deltaTime);
        if (transform.position.x == -7.5f)
        {
            Destroy(gameObject);
            GameController_SubwayMinigame2.instance.countHat--;
            if(GameController_SubwayMinigame2.instance.countHat == 1 && !GameController_SubwayMinigame2.instance.isStop)
            {
                GameController_SubwayMinigame2.instance.SpawnHat();
            }
        }
    }

    private void OnEnable()
    {
        SubwayObj_SubwayMinigame2.Event_ChangeSpeedBG += HandleOnChangeSpeedBG;
    }
    private void OnDisable()
    {
        SubwayObj_SubwayMinigame2.Event_ChangeSpeedBG -= HandleOnChangeSpeedBG;
    }
}
