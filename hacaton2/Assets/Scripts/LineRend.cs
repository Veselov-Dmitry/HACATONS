using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class LineRend : MonoBehaviour {

    public static LineRend Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Transform m_Traget1;
    public Transform m_Traget2;
    public Transform m_TragetSave;
    private LineRenderer lr;
	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
        m_TragetSave = m_Traget2;
    }

    public void SetHorse(Transform targetHorse)
    {
        m_Traget2 = targetHorse;
    }
    public void ResetTarget()
    {
        m_Traget2 = m_TragetSave;
    }

    // Update is called once per frame
    void Update () {
        var arr = new Vector3[] { m_Traget1.position, m_Traget2.position };
        lr.SetPositions(arr);

    }
}
