using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEndManager : MonoBehaviour
{
    public GameObject[] backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }

        backgrounds[UniteData.Difficulty - 1].SetActive(true);
    }
}
