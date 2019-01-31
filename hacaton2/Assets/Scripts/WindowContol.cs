using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowContol : MonoBehaviour {

    public GameObject m_Defeat;
    public GameObject m_Victory;
    public GameObject m_Pause;
    public GameObject m_Records;
    public static WindowContol Instance;


    private List<GameObject> dialogs = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        dialogs = new List<GameObject>() { m_Defeat, m_Victory, m_Pause, m_Records };
    }
    private void Off()
    {
        foreach (var item in dialogs)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1;
    }

    private void Set(GameObject OnObject, bool state = false)
    {
        foreach (var item in dialogs)
        {
            if (OnObject == item)
            {
                item.SetActive(true);
                continue;
            }
            item.SetActive(state);
        }
    }

    public void Devefeat()
    {
        Time.timeScale = 0;
        Set(m_Defeat);
    }
    public void Victory()
    {
        Time.timeScale = 0;
        Set(m_Victory);
    }
    public void Pause()
    {
        if (Time.timeScale ==1)
        {
            GameController.state = GameController.State.Pause;
            Time.timeScale = 0;
            Set(m_Pause);
        }
        else
        {
            GameController.state = GameController.State.Game;
            Off();
        }
    }
    public void Records()
    {
        Time.timeScale = 0;
        Set(m_Records);
    }
    public void RestartLevel()
    {
        GameController.Instace.RestartLevel();
    }
}
