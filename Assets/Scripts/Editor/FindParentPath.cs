using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

public class FindParentPath
{
    [MenuItem("GameObject/��ȡ����·��", false, -100)]
    static void Find()
    {
        GameObject selectedGameObject = Selection.activeGameObject;
        if (selectedGameObject) //���ѡ��
        {
            string path = "" + selectedGameObject.name;
            Transform parent = selectedGameObject.transform;
            while (true)
            {
                parent = parent.transform.parent;
                try
                {
                    if (parent == null|| parent.parent == null || parent.parent.name == "WndRoot" || parent.parent.name == "Canvas (Environment)")
                    {
                        break;
                    }
                }catch(Exception e)
                {

                }
                
                path = parent.name + "/" + path;
            }
            Debug.Log(path); 
            TextEditor t = new TextEditor();
            t.text = path;
            t.OnFocus();
            t.Copy();
        }
    }

    [MenuItem("GameObject/��ȡ����transform����", false, -100)]
    static void FindTransformData()
    {
        GameObject selectedGameObject = Selection.activeGameObject;
        if (selectedGameObject)
        {
            Debug.Log("position:" + selectedGameObject.transform.localPosition.x + "|" + selectedGameObject.transform.localPosition.y + "|" + selectedGameObject.transform.localPosition.z + "    " +
                "rotaxion:" + selectedGameObject.transform.localEulerAngles.x + "|" + selectedGameObject.transform.localEulerAngles.y + "|" + selectedGameObject.transform.localEulerAngles.z + "    " +
                "scale:" + selectedGameObject.transform.localScale.x + "|" + selectedGameObject.transform.localScale.y + "|" + selectedGameObject.transform.localScale.z + "    "
                );
        }
    }
}