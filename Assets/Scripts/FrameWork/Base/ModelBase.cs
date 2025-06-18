using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� �������� ���ݻ�ȡ ��������
/// </summary>
public class ModelBase
{
    public UIWindow uiWindow;
    public virtual void Init(UIWindow uiWindow)
    {
        this.uiWindow = uiWindow;
        RegisterListener();
    }
    public virtual void OnEnable()
    {

    }

    public virtual void OnDestory()
    {

    }

    public virtual void RegisterListener()
    {

    }
}
