using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasso : MonoBehaviour {

    public static Lasso Instance;
    private void Awake()
    {
        Instance = this;
    }
    public BoxCollider2D collliderBox;

    private void Start()
    {
        collliderBox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.gameObject.name);
        collision.GetComponent<HorseMover>().StopAnimator();
        collliderBox.enabled = false;
        GameController.Instace.AddHorse();
        LineRend.Instance.SetHorse(collision.transform);
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    print("OnTriggerStay2D");
    //    collision.GetComponent<HorseMover>().StopAnimator();
    //    collliderBox.enabled = false;
    //    GameController.Instace.AddHorse();
    //}
}
