using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class SignalTest : MonoBehaviour
{
    public PlayableDirector playable;
    public int index = 0;
    string[] strs = new string[6] {
        "��һ�仰��"
        ,"�ڶ��仰��"
        ,"�����仰��"
        ,"���ľ仰��"
        ,"����仰��"
        ,"�����仰��"};

    public Text text;
    public void TestFunc()
    {
        Debug.Log("�����µ�һ��ѭ��");
    }

    public void CubeSpeakFunc()
    {
        playable.Pause();
        StartCoroutine(SpeakFunc(strs[index]));
    }

    IEnumerator SpeakFunc(string str)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            stringBuilder.Append(str[i]);

            text.text = stringBuilder.ToString();
            yield return new WaitForSeconds(0.3f);
        }
        index++;
        playable.Play();
        yield return null;
    }
}
