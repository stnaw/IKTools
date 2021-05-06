using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace IK
{
public class IKToolsOSX : IKTools
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate bool DraggingEventCallBack(int statusType, string files, float focusPointX, float focusPointY);

    [AOT.MonoPInvokeCallback(typeof(DraggingEventCallBack))]
    private static bool draggingStatusChanged(int statusType, string files, float focusPointX, float focusPointY)
    {
        if(Instance.DraggingStatusChangedFunc == null) return false;

        return Instance.DraggingStatusChangedFunc.Invoke((DraggingStatusType)statusType, files.Split((char)28), new Vector2(focusPointX, focusPointY));
    }



    [DllImport("IKToolsOSX")]
    private static extern bool InitializeDragDropComponent(DraggingEventCallBack callback);

    [DllImport("IKToolsOSX")]
    private static extern bool ShowInFinder(string combinedPath);


    public override void InitializeDragDropComponent()
    {
        InitializeDragDropComponent(draggingStatusChanged);
    }

    public override void ShowInFolder(List<string> paths)
    {
        string combinedPath = string.Empty;
        paths.ForEach(s =>
        {
            combinedPath += $"{(char)28}{s}";
        });
        ShowInFinder(combinedPath);
    }



}
}
