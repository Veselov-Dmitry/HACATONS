using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController Instace ;
    public enum State
    {
        Game,End,
        Pause
    }
    public static State state;
    [SerializeField]
    Text m_Score;
    [SerializeField]
    Text m_Horses;
    [SerializeField]
    Text m_Time;

    int Horses;
    int Score;
    int CostHorse = 250;
    TimeSpan time;
    DateTime startTime;
    DateTime endTime;

    void Awake() {
        Instace = this;
        Score = Horses = 0;
        
        startTime = DateTime.Now;
        endTime = DateTime.Now.AddMinutes(2);
        m_Score.text = "0";
        m_Horses.text = "0";
        //PlayerPrefs.SetString("Records", "2500\n5500\n1500\n8500");
    }

    public void AddHorse()
    {
        Horses++;
        Score = Horses * CostHorse;

        m_Score.text = Score.ToString();
        m_Horses.text = Horses.ToString();
    }

	void Update () {
        if (endTime < DateTime.Now)
        {
            state = State.End;
            WindowContol.Instance.Victory();
            StopAllCoroutines();
            string val = PlayerPrefs.GetString("Records");
            val += "\n"+Score.ToString();
            PlayerPrefs.SetString("Records", val);
            return;
        }
        time = endTime - DateTime.Now;
        m_Time.text = time.Minutes.ToString("00:") + time.Seconds.ToString("00");

    }

    internal void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
