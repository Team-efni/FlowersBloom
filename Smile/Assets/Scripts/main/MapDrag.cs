using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDrag : MonoBehaviour
{
    private int map = 0; // 1:map1, 2:map2
    private bool chk = false;
    private Vector2 mousePos1, mousePos2;
    private Camera Camera;
    public GameObject MapCombine;
    public Animator animator;
    void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        MapCombine.GetComponent<RectTransform>().localPosition = Vector3.zero;
        if (MapCombine.GetComponent<RectTransform>().anchoredPosition.x == 0) map = 1;
        else if (MapCombine.GetComponent<RectTransform>().anchoredPosition.x == -3200) map = 2;
        else Debug.Log("MapCombine Pos Error");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Input.mousePosition;
            mousePos1 = Camera.ScreenToWorldPoint(mousePos1);
            chk = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            chk = false;
            mousePos2 = Input.mousePosition;
            mousePos2 = Camera.ScreenToWorldPoint(mousePos2);
        }

        if (!chk)
        {
            //Debug.Log("mouse1 : " + mousePos1.x + ", mouse2 : " + mousePos2.x);
            if(map == 1 && mousePos1.x > mousePos2.x + 500)
            {
                animator.SetInteger("LocalStage", 1);
                map = 2;
            }

            else if(map == 2 && mousePos1.x < mousePos2.x + 500)
            {
                animator.SetInteger("LocalStage", 2);
                map = 1;
            }
            mousePos1 = new Vector2();
            mousePos2 = new Vector2();
            chk = true;
        }
    }
}
