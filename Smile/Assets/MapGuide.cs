using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGuide : MonoBehaviour
{
    public GameObject guide;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("MapVisited") == 0) guide.SetActive(true);
        PlayerPrefs.SetInt("MapVisited", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            guide.SetActive(false);
        }
    }
}
