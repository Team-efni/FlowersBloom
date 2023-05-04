using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    [SerializeField]float speed;

    [SerializeField] float posValue;

    private Vector2 startPos;
    private float newPos;

    private bool gameClear;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        //Debug.Log("repeatBG_I : " + transform.position.x);
        //transform.position = GameObject.Find("OriginPos").transform.position;

        if (UniteData.Player_Location_Past != Vector2.zero && this.gameObject.name == "Floor2") 
        {
            //Debug.Log("Floor2 접근됨");
            startPos = UniteData.Player_Location_Past;
        }
        else
        {
            startPos = transform.position;
        }

        //Debug.Log("SP: "+startPos);

        gameClear = false;
        newPos = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameClear)
        {
            if(UniteData.Move_Progress)
            {
                newPos = Mathf.Repeat(Time.time * speed, posValue);
            }
            else
            {
                //일단 저장해두고 컷씬 클리어하면 Floor2를 이 위치로 불러옴
                UniteData.Player_Location_Past = startPos + Vector2.left * newPos - new Vector2(-9f * speed, 0);
            }
        }
        transform.position = (startPos + Vector2.left * newPos);
    }

    public void SetGameClearTrue()
    {
        //UniteData.Player_Location_Past = Vector2.zero;
        gameClear = true;
    }
}
