using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLose_SubwayMinigame2 : MonoBehaviour
{
    public SubwayObj_SubwayMinigame2 subwayObj;

    private void Start()
    {
        CheckAction();
    }

    public void CheckAction()
    {
        if (CompareTag("Box"))
        {
            Invoke(nameof(DelayBungTay), 0.5f);
        }
        else if (CompareTag("Tree"))
        {
            Invoke(nameof(Shoes), 0.5f);
        }
        else if (CompareTag("PuzzleClear"))
        {
            Invoke(nameof(UFO), 0.5f);
        }
    }

    public void DelayBungTay()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Shoes()
    {
        Vector3 startPos = transform.position;
        transform.DORotate(new Vector3(0, 0, 45), 0.5f);
        transform.DOMove(new Vector2(transform.position.x + 3, transform.position.y + 3), 0.5f).OnComplete(() => 
        {
            transform.DORotate(Vector3.zero, 0.5f);
            transform.DOMove(startPos, 0.5f);
        });
    }
    public void UFO()
    {
        transform.DOMoveY(transform.position.y + 3, 1).OnComplete(() =>
        {
            //transform.DOMoveX(subwayObj.transform.position.x - 2, 0.5f).OnComplete(() =>
            //{
            //    //chay anim UFO hut
            //});
        });
    }
}
