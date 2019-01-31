using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posit : MonoBehaviour {

    public int num;
    public static Action<int> OnHide;
    public static Action<int> OnShow;
    public void EndHide()
    {
        if (OnHide != null)
        {
            OnHide(num);
        }
    }
    public void EndShow()
    {
        if (OnShow != null)
        {
            OnShow(num);
        }
    }

}
