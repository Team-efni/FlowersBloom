using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    [SerializeField]float speed;

    [SerializeField] float posValue;

    private Vector2 startPos;
    private float newPos;

    public bool gameClear = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameClear)
            newPos = Mathf.Repeat(Time.time * speed * (UniteData.Move_Progress ? 1f : 0f), posValue);
        transform.position = (startPos + Vector2.left * newPos);
    }

    public void setGameClearTrue()
    {
        gameClear = true;
    }

}
