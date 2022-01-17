using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController_SubwayMinigame2 : MonoBehaviour
{
    public static GameController_SubwayMinigame2 instance;

    public bool isWin, isLose;
    public bool isStop;
    public Camera mainCamera;
    public List<Transform> listPosHat = new List<Transform>();
    public hat_SubwayMinigame2 hatPrefab;
    public List<hat_SubwayMinigame2> listCurrentHat = new List<hat_SubwayMinigame2>();
    public List<int> listCheckSame = new List<int>();
    public List<Sprite> listNumber;
    
    public int indexLane;
    public SubwayObj_SubwayMinigame2 subwayObj;
    public Vector2 startMousePos;
    public Vector2 endMousePos;
    public int countHat;
    public int countStage;
    public GameObject tutorial;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isLose = false;
        isStop = false;
    }


    private void Start()
    {
        SetSizeCamera();
        SpawnHat();
        countStage = 1;
        indexLane = 1;
        Tutorial();
    }

    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;
        
        mainCamera.orthographicSize *= f1 / f2;
    }

    void Tutorial()
    {
        tutorial.transform.position = new Vector2(-0.73f, 1.08f);
        tutorial.transform.DOMoveY(tutorial.transform.position.y - 4, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            tutorial.transform.DOMoveY(tutorial.transform.position.y + 4, 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (tutorial.activeSelf)
                {
                    Tutorial();
                };
            });
        });
    }

    public void SpawnHat()
    {
        listCheckSame.Clear();
        listCurrentHat.Clear();
        int[] a = { 0, 1, 2, 3 };
        listCheckSame.AddRange(a);
        for (int i = 0; i < 4; i++)
        {
            int ran = Random.Range(0, listCheckSame.Count);
            listCurrentHat.Add(Instantiate(hatPrefab, listPosHat[i].position, Quaternion.identity));
            listCurrentHat[i].spriteNumber.sprite = listNumber[listCheckSame[ran]];
            listCheckSame.RemoveAt(ran);
        }
        countHat = 4;
    }

    public void Win()
    {
        Debug.Log("Win");
        isWin = true;
        subwayObj.transform.DOMoveX(subwayObj.transform.position.x + 20, 3).OnComplete(() =>
        {
            Destroy(subwayObj.gameObject);
        });
    }

    public void Lose()
    {
        Debug.Log("Thua");
        isLose = true;

    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLose && !isWin && !isStop)
        {
            startMousePos = (Vector2)(mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position);
        }
        if (Input.GetMouseButtonUp(0) && !isLose && !isWin && !isStop)
        {
            endMousePos = (Vector2)(mainCamera.ScreenToWorldPoint(Input.mousePosition) - mainCamera.transform.position);

            if (startMousePos.x == endMousePos.x)
            {
                return;
            }
            else if (startMousePos.y + 0.1f < endMousePos.y && Mathf.Abs(startMousePos.x - endMousePos.x) < 5)
            {
                if (indexLane > 0)
                {
                    if (tutorial.activeSelf)
                    {
                        tutorial.SetActive(false);
                        tutorial.transform.DOKill();                    
                    }
                    Debug.Log("Len");
                    subwayObj.GetTop();
                    indexLane--;
                }
            }
            else if (startMousePos.y > endMousePos.y + 0.1f && Mathf.Abs(startMousePos.x - endMousePos.x) < 5)
            {
                if (indexLane < 3)
                {
                    if (tutorial.activeSelf)
                    {
                        tutorial.SetActive(false);
                        tutorial.transform.DOKill();
                    }
                    Debug.Log("Xuong");
                    subwayObj.GetBot();
                    indexLane++;
                }
            }
        }
    }

}