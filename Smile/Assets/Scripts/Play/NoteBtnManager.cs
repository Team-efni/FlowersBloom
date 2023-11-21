using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteBtnManager : MonoBehaviour
{
    private NoteController noteManager;

    public NoteController noteManager_easy;
    public NoteController noteManager_normal;
    public NoteController noteManager_hard;
    public NoteController noteManager_world2_easy;
    public NoteController noteManager_world2_normal;
    public NoteController noteManager_world2_hard;
    public NoteController noteManager_end;
    public Button[] Notes;

    // Start is called before the first frame update
    void Start()
    {
        switch(UniteData.Difficulty)
        {
            case 1:
                noteManager = noteManager_easy;
                break;
            case 2:
                noteManager = noteManager_normal;
                break;
            case 3:
                noteManager = noteManager_hard;
                break;
            case 4:
                noteManager = noteManager_world2_easy;
                break;
            case 5:
                noteManager = noteManager_world2_normal;
                break;
            case 6:
                noteManager = noteManager_world2_hard;
                break;
            case 7:
                noteManager = noteManager_end;
                break;
            default:
                noteManager = noteManager_easy;
                break;
        }
        
        for (int i = 0; i<Notes.Length; i++)
        {
            int index = i;

            EventTrigger eventTrigger = Notes[i].gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
            pointerDownEntry.eventID = EventTriggerType.PointerDown;
            pointerDownEntry.callback.AddListener((eventData) => { OnLongTouchDown(index); });
            eventTrigger.triggers.Add(pointerDownEntry);

            EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
            pointerUpEntry.eventID = EventTriggerType.PointerUp;
            pointerUpEntry.callback.AddListener((eventData) => { OnLongTouchUp(index); });
            eventTrigger.triggers.Add(pointerUpEntry);

            Notes[i].gameObject.SetActive(true);
            Notes[i].onClick.RemoveAllListeners();
            Notes[i].onClick.AddListener(() => OnTouchClick(index));
        }

        /*
        Notes[0].onClick.AddListener(OntouchClickLeftUp);
        Notes[1].onClick.AddListener(OntouchClickLeftDown);
        Notes[2].onClick.AddListener(OntouchClickRightUp);
        Notes[3].onClick.AddListener(OntouchClickRightDown);
        */
    }

    private void OnTouchClick(int num)
    {
        noteManager.touchClick(num);
    }

    private void OnLongTouchDown(int num)
    {
        noteManager.LongTouchDown(num);
    }

    private void OnLongTouchUp(int num)
    {
        Debug.Log("123123");
        noteManager.LongTouchUp(num);
    }
    /*
    private void OntouchClickLeftUp()
    {
        noteManager.touchClickLeftUp();
    }

    private void OntouchClickLeftDown()
    {
        noteManager.touchClickLeftDown();
    }
    private void OntouchClickRightUp()
    {
        noteManager.touchClickRightUp();
    }
    private void OntouchClickRightDown()
    {
        noteManager.touchClickRightDown();
    }*/
}
