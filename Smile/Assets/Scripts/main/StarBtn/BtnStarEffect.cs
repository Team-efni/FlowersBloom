using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BtnStarEffect : MonoBehaviour
{
    public GameObject star;
    public List<GameObject> btns;
 
    void Start()
    {
        foreach (GameObject btn in btns) 
        {
             btn.GetComponent<Button>().onClick.AddListener(call: () => ClickStarEffect(btn));
        }
        
    }



    // Update is called once per frame
    void ClickStarEffect(GameObject _gameObject)
    {
        Instantiate(star, _gameObject.transform.position, rotation: (Quaternion)quaternion.identity);
    }

    void OneClick4Star(GameObject _gameObject)
    {
        Instantiate(star, _gameObject.transform.position, rotation: (Quaternion)quaternion.identity);
        Instantiate(star, _gameObject.transform.position, rotation: (Quaternion)quaternion.identity);
        Instantiate(star, _gameObject.transform.position, rotation: (Quaternion)quaternion.identity);
        Instantiate(star, _gameObject.transform.position, rotation: (Quaternion)quaternion.identity);
    }
}
