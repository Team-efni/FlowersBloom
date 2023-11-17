using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UI_Comment : MonoBehaviour
{

    //Setting Object's alpha value
    public void fadeOutImage(Image image, int cycleFrame, int nowFrame)
    {
        if (cycleFrame < nowFrame)
        {
            Debug.LogError("[UI_Comment.cs/Func: fadeOutImage]\tthe (nowFrame) parameter is bigger than (cycleFrame) parameter.");
            return;
        }
        if (nowFrame < 0)
        {
            Debug.LogError("[UI_Comment.cs/Func: fadeOutImage]\tthe (nowFrame) parameter value is below than 0.");
            return;
        }

        Color colorB = image.color;
        colorB.a = 1.0f * nowFrame / cycleFrame;
        image.color = colorB;
    }

    public void fadeOutText(Text text, int cycleFrame, int nowFrame)
    {
        if (cycleFrame < nowFrame)
        {
            Debug.LogError("[UI_Comment.cs/Func: fadeOutText]\tthe (nowFrame) parameter is bigger than (cycleFrame) parameter.");
            return;
        }
        if (nowFrame < 0)
        {
            Debug.LogError("[UI_Comment.cs/Func: fadeOutText]\tthe (nowFrame) parameter value is below than 0.");
            return;
        }
        Color colorT = text.color;
        colorT.a = 1.0f * nowFrame / cycleFrame;
        text.color = colorT;
    }

    //setting text printing
    public int printTextToUI(Text text, string sentence, int frame, int printPerFrame, bool printImmediately)
    {
        string processingSentence = sentence;
        if(!printImmediately) 
        {
            if (frame / printPerFrame <= replaceClamp(sentence).Length && printPerFrame != 0)  
            {
                List<string> textInClampList = textInClamp(sentence);
                processingSentence = textProcessing(replaceClamp(sentence), frame / printPerFrame, textInClampList);
                text.text = processingSentence;
                return 0;
            }
            else
            {
                text.text = processingSentence;
                return 1;
            }
        }

        text.text = processingSentence;
        return 1;
    }

    private string textProcessing(string text, int textEnd, List<string> textInClampList)
    { 
        StringBuilder sb = new StringBuilder();
        int clampCount = 0;

        for (int x=0; x<textEnd; x++)
        {
            string t = text[x].ToString();

            if (t == "閄") 
            {
                sb.Append(textInClampList[clampCount++]);
            }
            else
            {
                sb.Append(t);
            }   
        }

        for(int x= clampCount; x< textInClampList.Count; x++)
        {
            sb.Append(textInClampList[x]);
        }

        return sb.ToString();
    }

    private List<string> textInClamp(string text)
    {
        List<string> results = new List<string>();
        int start, end = 0;

        while ((start = text.IndexOf('<', end)) != -1 && (end = text.IndexOf('>', start)) != -1)
        {
            string result = text.Substring(start, end - start + 1);
            results.Add(result);
        }

        return results;
    }

    private string replaceClamp(string text)
    {
        int start, end = 0;

        while ((start = text.IndexOf('<')) != -1)
        {
            end = text.IndexOf('>', start);
            string oldSubstring;
            if (end != -1)
                oldSubstring = text.Substring(start, end - start + 1);
            else
            {
                end = text.Length;
                oldSubstring = text.Substring(start, end - start);
            }
            text = text.Replace(oldSubstring, "閄");
        }
        return text;
    }
}
