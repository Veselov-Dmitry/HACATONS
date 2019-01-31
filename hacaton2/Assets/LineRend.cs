using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRend : MonoBehaviour {

    public Transform m_Traget1;
    public Transform m_Traget2;
    private LineRenderer lr;
	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        var arr = new Vector3[] { m_Traget1.position, m_Traget2.position };
        lr.SetPositions(arr);

    }
}
