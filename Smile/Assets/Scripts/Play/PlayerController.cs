using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;

    public node nd;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            // ∏ÛΩ∫≈Õø° ¥Í¿∏∏È ∏ÿ√ﬂ∞Ì RN æ¿ »£√‚
            moveSpeed = 0;
            Debug.Log("Go to RN");

            node.start_cutscene = true;
            animator.SetBool("start_cutscene", node.start_cutscene);
            nd.Start_Cutscene_Mode();
        }
    }
}
