using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiWindow : MonoBehaviour
{
    public static List<UiWindow> OpenedUI = new List<UiWindow>();

    protected virtual bool CloseOnCancel => true;

    public virtual void CancelAction()
    {
        if (CloseOnCancel && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
    
    protected virtual void OnEnable()
    {
        if (CloseOnCancel) OpenedUI.Add(this);
    }

    protected virtual void OnDisable()
    {
        if (CloseOnCancel) OpenedUI.Remove(this);
    }
}
