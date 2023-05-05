using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stage : MonoBehaviour
{
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Title;
    [SerializeField] GameObject Ping;

    public void LockMap()
    {
        Button.GetComponent<Button>().interactable = false;
        /* 타이틀, 핑 비활성화 처리 예정*/
    }
    
    public void UnlockMap()
    {
        Button.GetComponent<Button>().interactable = true;
    }
    


}
