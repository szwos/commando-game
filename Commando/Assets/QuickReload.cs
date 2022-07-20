using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickReload : MonoBehaviour
{
    public RectTransform track;
    public RectTransform thumb;

    public void setRange(int range, int beginning)
    {
        thumb.localScale = new Vector3(range/100.0f, 1, 1);
        
        
        //thumb.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, range);
       // thumb.SetPositionAndRotation(new Vector3(/*1.0f / beginning*/0.1f, 0f, 0f), thumb.rotation);
       // Debug.Log( (1.0f / beginning).ToString() + " beginning: " + beginning.ToString());
        thumb.localPosition = new Vector3(beginning / 100.0f - 0.5f , 0.0f, 0.0f);
    }
    
    public void setProgress(int progress)
    {
        //track.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, progress);
        track.localScale = new Vector3(progress / 100.0f, 1, 1);
    }
}
