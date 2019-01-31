using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    Transform Target;

    void Update()
    {
        transform.LookAt(Target, Vector3.forward);
    }
}
