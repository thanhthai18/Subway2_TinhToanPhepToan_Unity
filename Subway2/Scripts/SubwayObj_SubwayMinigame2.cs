using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayObj_SubwayMinigame2 : MonoBehaviour
{
    public AirplaneBoingMathController_MiniGame2 math;
    public GameObject crashVFXPrefab;
    public GameObject VFXPrefab;
    public bool isCanCheck;
    public ParticleSystem dung, sai;
    public SpriteRenderer tableNumber;
    public List<EffectLose_SubwayMinigame2> listLosePrefab = new List<EffectLose_SubwayMinigame2>();
    public MyBackGround_SubwayMinigame2 background;
    public static event Action<float> Event_ChangeSpeedBG;


    private void Awake()
    {
        isCanCheck = true;
    }

    void DelayCheck()
    {
        isCanCheck = true;
    }

    public void GetTop()
    {
        transform.DOMoveY(transform.position.y + 1.5f, 0.1f);
    }

    public void GetBot()
    {
        transform.DOMoveY(transform.position.y - 1.5f, 0.1f);
    }

    void DelayLose()
    {
        GameController_SubwayMinigame2.instance.Lose();
    }

    void DelayBungTay()
    {
        transform.DOPunchPosition(new Vector3(0.2f, 0.2f, 0.2f), 1);
        transform.DOMoveX(transform.position.x - 10, 3);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn") && isCanCheck)
        {
            isCanCheck = false;
            Invoke(nameof(DelayCheck), 3);
            var tmpCrash = Instantiate(crashVFXPrefab, new Vector2(transform.position.x + 0.7f, transform.position.y - 0.3f), Quaternion.identity);
            tmpCrash.transform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 0.8f).OnComplete(() =>
              {
                  Destroy(tmpCrash);
              });

            if (math.result == int.Parse(collision.GetComponent<hat_SubwayMinigame2>().spriteNumber.sprite.name))
            {
                Debug.Log("dung");
                Complete(collision.gameObject);
            }
            else
            {
                Debug.Log("sai");
                Fail(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            Debug.Log("ban tay");
            Invoke(nameof(DelayBungTay), 0.5f);
            Invoke(nameof(DelayLose), 0.5f);
            
        }
        else if (collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("giay");
            Invoke(nameof(DelayBungTay), 1.5f);
            Invoke(nameof(DelayLose), 1.5f);
        }
        else if (collision.gameObject.CompareTag("PuzzleClear"))
        {
            Debug.Log("UFO");
            transform.DOMoveY(transform.position.y, 2).OnComplete(() =>
            {
                transform.DOMove(new Vector2(collision.transform.position.x,collision.transform.position.y), 1).SetEase(Ease.Linear);
                transform.DOScale(Vector3.zero, 1).SetEase(Ease.Linear).OnComplete(() => 
                {
                    DelayLose();
                });
            });

        }
        else if (collision.gameObject.CompareTag("People"))
        {
            Debug.Log("vien da");
            sai.gameObject.SetActive(true);
            sai.Emit(0);
            DelayLose();
        }
    }



    void Complete(GameObject hat)
    {
        GameController_SubwayMinigame2.instance.countStage++;
        hat.SetActive(false);
        dung.gameObject.SetActive(true);
        dung.Emit(0);
        var tmpVFX = Instantiate(VFXPrefab, hat.transform.position, Quaternion.identity);
        tmpVFX.GetComponent<SpriteRenderer>().DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() =>
         {
             Destroy(tmpVFX);
             dung.gameObject.SetActive(false);
         });

        if (GameController_SubwayMinigame2.instance.countStage <= 12)
        {
            if(GameController_SubwayMinigame2.instance.countStage == 4)
            {
                background.speedBG++;
                Event_ChangeSpeedBG?.Invoke(background.speedBG);
            }
            else if(GameController_SubwayMinigame2.instance.countStage == 8)
            {
                background.speedBG++;
                Event_ChangeSpeedBG?.Invoke(background.speedBG);
            }
            else if(GameController_SubwayMinigame2.instance.countStage == 12)
            {
                background.speedBG++;
                Event_ChangeSpeedBG?.Invoke(background.speedBG);
            }
            tableNumber.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                math.result = 0;
                tableNumber.transform.DOScale(new Vector3(4.5f, 4.5f, 4.5f), 0.5f).SetEase(Ease.Linear);
            });
        }
        else
        {
            GameController_SubwayMinigame2.instance.Win();
        }    
    }

    void Fail(GameObject hat)
    {
        hat.SetActive(false);
        GameController_SubwayMinigame2.instance.isStop = true;
        var tmpFail = Instantiate(listLosePrefab[UnityEngine.Random.Range(0, listLosePrefab.Count)], hat.transform.position, Quaternion.identity);
    }

    
}
