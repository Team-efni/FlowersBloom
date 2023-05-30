using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector2 direction;
    public float moveSpeed = 0.005f;
    public float sizeSpeed = 1f;
    public float colorSpeed = 3f;
    public float maxSize = 0.5f;
    public float minSize = 0.3f;
    public Color[] colors;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        direction = new Vector2(x: Random.Range(-1.0f, 1.0f), y: Random.Range(-1.0f, 1.0f));
        float size = Random.Range(minSize, maxSize);
        transform.localScale = (Vector3)new Vector2(x: size, y: size);
        sr.color = colors[Random.Range(0, colors.Length)];

    }

    // Update is called once per frame
    void Update()
    {
        // 이동
        transform.Translate(translation: (Vector3)(direction * moveSpeed));
        // 크기
        transform.localScale = (Vector3)Vector2.Lerp(a: (Vector2)transform.localScale, b: Vector2.zero, t: Time.deltaTime * sizeSpeed);
        // 투명
        Color color = sr.color;
        color.a = Mathf.Lerp(sr.color.a, b:0, t: Time.deltaTime * colorSpeed);
        sr.color = color;
        // 파괴
        if (sr.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
      

    }
}
