using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Resords : MonoBehaviour {

    public Text m_Row;
    public Transform m_TextRoot;
    private void OnEnable()
    {
        string rows = PlayerPrefs.GetString("Records");
        string[] arr = rows.Split('\n');
        List<int> values = new List<int>();
        foreach (var item in arr)
        {
            values.Add(int.Parse(item));
        }
        values = values.OrderByDescending(x=>x).ToList();
        int i = 1;
        foreach (var item in values)
        {
            var text = Instantiate(m_Row, m_TextRoot);
            text.text = i++ + " " + item;            
        }
    }
}
