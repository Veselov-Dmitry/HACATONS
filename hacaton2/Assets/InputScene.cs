using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScene : MonoBehaviour {
    private object lockObj;
    private float curTouchTime;
    private bool isTouch;
    private Vector3 EndMouse;
    private float minTouchTime = 0.1f;

    public Vector3 StartMouse
    {
        get;
        private set;
    }

    public GameObject m_Lasso;
    private Rigidbody2D rig;
    void Start () {
        rig = gameObject.GetComponent<Rigidbody2D>();

    }

    internal static Vector3 ScreenToCenterScreen(Vector3 pos)
    {
        return new Vector3(pos.x - Screen.width / 2, pos.y - Screen.height / 2, 0);
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonUp(0)&& lockObj != null)
        {
            lockObj = null;
            isTouch = false;
            EndMouse = Input.mousePosition;
            SetForce(StartMouse,EndMouse, Time.time - curTouchTime);
        }
        /*int fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;
        }
        if (fingerCount > 0)*/
        if (Input.GetMouseButton(0))
        {
            if (lockObj == null)
            {
                StartMouse = Input.mousePosition;
                lockObj = new object();
                curTouchTime = Time.time;
                //OnGetFirstTouch.Invoke(mouse);
                //SaveCall(OnGetFirstTouch, mouse);
            }
            isTouch = true;
            if (Time.time - curTouchTime > minTouchTime)
            {
                //SaveCall(OnHitFooter, isHitToFooter());
                //SaveCall(OnGetTouch, mouse);
            }
        }
    }
    private void SetForce(Vector3 start,Vector3 endMouse, float time)
    {
        //Debug.Break();
        StartCoroutine(Move(start,endMouse, time));
    }

    private IEnumerator Move(Vector3 start, Vector3 endMouse, float time)
    {
        //start.y -= 4;
        gameObject.transform.position = start;
        var mult= (5+1 / time) * 200;
        float end = (endMouse - start).magnitude;
        print("++++++++"+end);
        float val = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            val += Time.deltaTime*mult;
            //print(val);
            if (val >= end)
            {
                val = end;
                Vector3.MoveTowards(m_Lasso.transform.position, endMouse, val);
                break;
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(Vector3.MoveTowards(start, endMouse, val));
            print("pos=" + pos);
            m_Lasso.transform.position = pos;
        }
    }
}
