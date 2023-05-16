using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIInit : MonoBehaviour
{
    public GameObject UI_Character;

    public Sprite 민들레_사진;
    public Sprite 튤립_사진;

    private void Update()
    {
        if (UniteData.Selected_Character == "Dandelion")
        {
            UI_Character.GetComponent<Image>().sprite = 민들레_사진;
        }
        else if (UniteData.Selected_Character == "Tulip")
        {
            UI_Character.GetComponent<Image>().sprite = 튤립_사진;
        }
    }
}
