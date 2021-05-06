using System.Collections;
using System.Collections.Generic;
using System.IO;
using IK;
using UnityEngine;

public class sample2 : MonoBehaviour
{
    private void OnGUI()
    {
        if(GUI.Button(new Rect((float)(Screen.width - 300) / 2f, (float)(Screen.height - 100) / 2f, 300, 100), "Generate File And Show In Finder"))
        {
            string path = $"{Application.temporaryCachePath}/sample2.txt";
            File.WriteAllText(path, "sample2");
            IKTools.Instance.ShowInFolder(new List<string> {path});
        }
    }
}
