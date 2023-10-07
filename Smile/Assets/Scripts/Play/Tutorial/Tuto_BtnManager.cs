using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto_BtnManager : MonoBehaviour
{
    public Tuto_NoteController noteManager_world_tuto;
    public Button[] Notes_Btn;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Notes_Btn.Length; i++)
        {
            int index = i;

            Notes_Btn[i].gameObject.SetActive(true);
            Notes_Btn[i].onClick.RemoveAllListeners();
            Notes_Btn[i].onClick.AddListener(() => OnTouchClick(index));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTouchClick(int num)
    {
        noteManager_world_tuto.touchClick(num);
    }


}
