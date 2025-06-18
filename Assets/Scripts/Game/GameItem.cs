using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class GameItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //��¼�����������ɫ
    Color[] stringArr = new Color[6]
    {
       Color.red,Color.green,Color.blue,Color.gray,Color.yellow,Color.black
    };
    Image image;//չʾ����Ʒ����
    public int colorIndex;//��ǰ����
    bool isDrag;//�Ƿ�����ק
    GameView gameView;
    Vector3 nowPos;
    int num = 9;
    public int index;
    public Vector3 startPos;
    Vector3 targetPos = Vector3.zero;
    int targetIndex = 0;
    Text text;
    public void Init(GameView gameView, int index, int localIndex)
    {
        this.index = localIndex;
        Random random = new Random();
        image = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        startPos = transform.localPosition;
        this.gameView = gameView;
        ChangeColor(index);
    }

    /// <summary>
    /// �޸���ɫ�ķ���
    /// </summary>
    public void ChangeColor(int index)
    {
        //�����ʾ��Ʒ
        colorIndex = index;
        image.color = stringArr[index];
        text.text = colorIndex.ToString() + "\n" + this.index.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //����boolֵ
        isDrag = true;
        nowPos = transform.position;
        targetIndex = -1;
        gameView.dic.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�������
        if (isDrag&&targetIndex==-1)
        {
            // ��ȡ������Ļλ��
            Vector3 mousePosition = Input.mousePosition;

            // ��������Ļ����ת��Ϊ�ӿ�����
            Vector3 viewportPosition = Camera.main.ScreenToViewportPoint(mousePosition);

            // ���ӿ�����ת��Ϊ��������
            Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);

            // ֻ����x, y��λ�ã�z�������Ҫ����
            worldPosition.z = transform.position.z;
            //Debug.Log(Math.Abs(worldPosition.x - nowPos.x));
            //���ݲ�ֵ�����Ǻ�����ק����������ק
            if (Math.Abs(worldPosition.x - nowPos.x) > Math.Abs(worldPosition.y - nowPos.y))
            {
                worldPosition.y = nowPos.y;
                if (worldPosition.x - nowPos.x > 0.2f && index % num != 8)
                {
                    targetPos = startPos + new Vector3(100, 0, 0);
                    targetIndex = index + 1;
                }
                else if (worldPosition.x - nowPos.x < -0.2f && index % 9 != 0)
                {
                    targetPos = startPos + new Vector3(-100, 0, 0);
                    targetIndex = index-1;
                }
            }
            if (Math.Abs(worldPosition.x - nowPos.x) < Math.Abs(worldPosition.y - nowPos.y))
            {
                worldPosition.x = nowPos.x;
                if (worldPosition.y - nowPos.y > 0.2f && index < num * (num - 1))
                {
                    targetPos = startPos + new Vector3(0, 100, 0);
                    targetIndex = index + num;
                }
                else if (worldPosition.y - nowPos.y < -0.2f && index >= num)
                {
                    targetPos = startPos + new Vector3(0, -100, 0);
                    targetIndex = index -num;
                }
            }
            if (targetIndex >= 0)
            {
                transform.DOLocalMove(targetPos, 0.3f);
                gameView.itemObjs[targetIndex].transform.DOLocalMove(-targetPos, 0.3f);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        if(targetIndex >= 0)
        {
            DOTween.Kill(transform, true);
            DOTween.Kill(gameView.itemObjs[targetIndex].transform, true);
            ChangeItem();
            if (!gameView.GetClear(colorIndex, this, gameView.itemObjs[targetIndex].colorIndex, gameView.itemObjs[targetIndex]))
            {
                ChangeItem();
                transform.DOLocalMove(Vector3.zero, 0.3f);
                gameView.itemObjs[targetIndex].transform.DOLocalMove(Vector3.zero, 0.3f);
            }
            else
            {
                gameView.ClearDic();
            }
        }

    }

    private void ChangeItem()
    {
        transform.localPosition = Vector3.zero;
        gameView.itemObjs[targetIndex].transform.localPosition = Vector3.zero;
        int localColorIndex = colorIndex;
        ChangeColor(gameView.itemObjs[targetIndex].colorIndex);
        gameView.itemObjs[targetIndex].ChangeColor(localColorIndex);
    }
}
