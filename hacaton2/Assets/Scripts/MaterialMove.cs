using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMove : MonoBehaviour {

    [Range(1, 100)]
    [SerializeField]
    float Speed;
    LineRenderer rend;

    void Start()
    {
        rend = GetComponent<LineRenderer>();
    }

    void Update()
    {
        float offset = Time.time * Speed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
