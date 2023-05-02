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
        Debug.Log("repeatBG_I : " + transform.position.x);
        transform.position = GameObject.Find("OriginPos").transform.position;
        startPos = transform.position;
        gameClear = false;
        newPos = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameClear)
            newPos = Mathf.Repeat(Time.time * speed * (UniteData.Move_Progress ? 1f : 0f), posValue);
        transform.position = (startPos + Vector2.left * newPos);
    }

    public void SetGameClearTrue()
    {
        gameClear = true;
    }
}
