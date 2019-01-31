using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HorseMover : MonoBehaviour {

    Animator anim;
    Random rand;
    public int state;

    void Start () {
        anim = GetComponent<Animator>();
        
        Invoke("PlayAnimator", Random.Range(0, 5));
    }
    [ContextMenu("Pause+++")]
    public void StopAnimator()
    {
        anim.speed = 0;
        StartCoroutine(Catch());
    }
    [SerializeField]
    Transform m_Root;
    private IEnumerator Catch()
    {
        InputScene.Instance.lassoState = false;
        Lasso.Instance.gameObject.transform.parent.gameObject.SetActive(false);

        Vector2 endMouse = m_Root.position;
        Vector2 start = transform.position;
        anim.enabled = false;
        transform.position = start;
        print("start=" + start);
        float end = (endMouse -start).magnitude;
        print("end=" + end);
        float mult = 4f;
        float val = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            val += Time.deltaTime * mult;
            //print(val);
            if (val >= end)
            {
                val = end;
                Vector3.MoveTowards(transform.position, endMouse, val);
                break;
            }
            Vector2 pos = Vector3.MoveTowards(start, endMouse, val);
            print("pos=" + pos);
            transform.position = pos;
        }

        LineRend.Instance.ResetTarget();
        InputScene.Instance.lassoState = true;
        PlayAnimator();
    }

    [ContextMenu("Play+++")]
    public void PlayAnimator()
    {
        anim.enabled = true;
        anim.speed = 1;
        state = Random.Range(1, 6);
        anim.SetTrigger("way" + state);
    }
    public void EndAnimation()
    {
        state = Random.Range(1, 6);
        anim.SetTrigger("way" + state);
    }

}
