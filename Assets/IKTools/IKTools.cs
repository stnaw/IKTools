using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace IK
{
public enum DraggingStatusType
{
    None = 0,
    Entered = 1,
    Moved = 2,
    Ended = 3,
    Canceled = 4
}
public class IKTools
{

    private static IKTools instance = null;
    private static readonly object obj = new object();

    public static IKTools Instance
    {
        get
        {
            lock (obj)
            {
                if (instance == null)
                {
#if UNITY_STANDALONE_OSX
                    instance = new IKToolsOSX();
#else
                    instance = new IKToolsBase();
#endif
                }

                return instance;
            }
        }
    }

    public Func<DraggingStatusType, string[], Vector2, bool> DraggingStatusChangedFunc;

    public virtual void InitializeDragDropComponent() {}

    public virtual void ShowInFolder(List<string> paths) {}







}

}
