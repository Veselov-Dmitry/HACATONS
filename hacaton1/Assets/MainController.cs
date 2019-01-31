using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public int Length = 6;
    public GameObject[] m_H;
    public Button[] m_Buttons;
    public Animator[] m_Animator;
    public Text m_Sc;
    public Text m_L;
    public int m_Score = 0;
    public int m_Life = 5;
    public bool[] m_State ;
    private float delay = 2f ;

    System.Random rand1 = new System.Random();
    System.Random rand2 = new System.Random();

    private void OnEnable()
    {
        Posit.OnHide += Posit_OnHide;
        Posit.OnShow += Posit_OnShow;
    }

    private void Posit_OnShow(int pos)
    {
        m_State[pos] = true;
    }

    private void Posit_OnHide(int pos)
    {
        m_State[pos] = false;
    }

    void Start()
    {
        m_State = new bool[m_H.Length];
           m_Animator = new Animator[m_H.Length];
        for (int i = 0; i < m_H.Length; i++)
        {
            m_Animator[i] = m_H[i].GetComponent<Animator>();
        }
        StartCoroutine(MainLoop());
        m_Sc.text = "Score:" + m_Score;
        m_L.text = "Life:" + m_Life;
    }

    public void Push(int num)
    {
        if (m_State[num])
        {
            m_State[num] = false;
            m_Animator[num].SetTrigger("hide");
            m_Score++;
            m_Sc.text = "Score:"+ m_Score;
        }
        else
        {
            m_Life--;
            m_L.text = "Life:" + m_Life;

        }
    }

    private IEnumerator MainLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            int dig1=  rand1.Next(0,6);
            int dig2 = 0;
            while (dig2 == dig1)
            {
                dig2 = rand2.Next(0, 6);
            }
            for (int i = 0; i < m_H.Length; i++)
            {
                if (m_State[i] == false)
                {
                    if (i == dig1 && m_State[i] == false )
                    {
                        m_State[i] = true;
                        m_Animator[i].SetTrigger("show");
                        continue;
                    }
                    if (i == dig2 && m_State[i] == false)
                    {
                        m_State[i] = true;
                        m_Animator[i].SetTrigger("show");
                        continue;
                    }
                }
                else
                {
                    m_State[i] = false;
                    m_Animator[i].SetTrigger("hide");
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
