using System.Collections;
using System.Collections.Generic;
using System.IO;
using IK;
using UnityEngine;
using UnityEngine.UI;

public class sample1 : MonoBehaviour
{
    public RawImage imagePreview;


    public Text debugPreview;


    // Start is called before the first frame update
    void Start()
    {
        IKTools.Instance.InitializeDragDropComponent();
        IKTools.Instance.DraggingStatusChangedFunc += (eventType, paths, focus) =>
        {
            if(eventType == DraggingStatusType.Ended && paths.Length > 0)
            {
                string path = paths[0];
                Texture2D t = new Texture2D(2, 2);

                if(t.LoadImage(File.ReadAllBytes(path)))
                {
                    if(imagePreview.texture != null)
                    {
                        Texture2D.DestroyImmediate(imagePreview.texture);
                        imagePreview.texture = null;
                    }

                    imagePreview.texture = t;
                }
                else
                {
                    Texture2D.DestroyImmediate(t);
                }
            }

            debugPreview.text = $"dragging status:{eventType} focusPoint:{focus} \n path:{paths[0]}";
            return true;
        };
    }


}
