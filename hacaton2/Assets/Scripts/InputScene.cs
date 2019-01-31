using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScene : MonoBehaviour {
    private object lockObj;
    private float curTouchTime;
    private bool isTouch;
    private Vector3 EndMouse;
    public Vector3 StartMouse;
    private float endTouchTime;
    private float minTouchTime = 0.7f;
    public Action<bool> OnEnableLasso;

    public static InputScene Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject m_Lasso;
    public GameObject m_LineRend;
    public BoxCollider2D m_ObjectCollider;
    public bool lassoState;

    void Start ()
    {
        m_ObjectCollider.enabled = false;
        OnEnableLassoMethod(false);
        OnEnableLasso += OnEnableLassoMethod;
    }

    private void OnEnableLassoMethod(bool obj)
    {
        m_Lasso.SetActive(obj);
        m_LineRend.SetActive(obj);
    }

    void Update ()
    {
        if (GameController.state == GameController.State.End)
        {
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            WindowContol.Instance.Pause();
        }
        if (Input.GetMouseButtonUp(0)&& lockObj != null)
        {
            lockObj = null;
            isTouch = false;
            EndMouse = Input.mousePosition;
            endTouchTime = Time.time;
            SetForce(StartMouse,EndMouse, Time.time - curTouchTime);
            if (OnEnableLasso != null)
            {
                LineRend.Instance.ResetTarget();
                lassoState = true;
                OnEnableLasso(lassoState);
            }
            return;
        }
        if (lassoState && Time.time - endTouchTime > minTouchTime)
        {
            if (OnEnableLasso != null)
            {
                lassoState = false;
                OnEnableLasso(lassoState);
            }
        }

#if UNITY_ANDROID
        int fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;
        }
        if (fingerCount > 0) 
#elif UNITY_EDITOR
        if (Input.GetMouseButton(0))
#endif
        {
            if (lockObj == null)
            {
                StartMouse = Input.mousePosition;
                lockObj = new object();
                curTouchTime = Time.time;
            }
            isTouch = true;
        }
    }
    private void SetForce(Vector3 start,Vector3 endMouse, float time)
    {
        StartCoroutine(Move(start,endMouse, time));
    }

    private IEnumerator Move(Vector3 start, Vector3 endMouse, float time)
    {
        //Debug.Break();
        gameObject.transform.position = start;
        var mult= (5+1 / time) * 200;
        float end = (endMouse - start).magnitude;
        //print("++++++++"+end);
        float val = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            val += Time.deltaTime*mult;
            //print(val);
            if (val >= end)
            {
                CheckHorse();
                val = end;
                Vector3.MoveTowards(m_Lasso.transform.position, endMouse, val);
                break;
            }
            Vector2 pos = Camera.main.ScreenToWorldPoint(Vector3.MoveTowards(start, endMouse, val));
            //print("pos=" + pos);
            m_Lasso.transform.position = pos;
        }
    }

    private void CheckHorse()
    {
        m_ObjectCollider.enabled = true;
    }
}
