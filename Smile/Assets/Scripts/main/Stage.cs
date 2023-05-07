using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stage : MonoBehaviour
{
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Title;
    [SerializeField] GameObject Ping;

    private Color off_color = new Color(168/255f, 168 / 255f, 168 / 255f);
    private Color off_color_Btn = new Color(196/255f, 196/255f, 196/255f, 0/255f);

    private Color on_color = new Color(1, 1, 1, 1);
    private Color on_color_Btn = new Color(1, 1, 1, 0);

    public void LockMap()
    {
        Button.GetComponent<Button>().interactable = false;
       
        Button.GetComponent<Image>().color = off_color_Btn;
        Title.GetComponent<Image>().color = off_color;
        Ping.GetComponent<Image>().color = off_color;


        /* 타이틀, 핑 비활성화 처리 예정*/
    }
    
    public void UnlockMap()
    {
        Button.GetComponent<Button>().interactable = true;

        Button.GetComponent<Image>().color = on_color_Btn;
        Title.GetComponent<Image>().color = on_color;
        Ping.GetComponent<Image>().color = on_color;
    }
    


}
