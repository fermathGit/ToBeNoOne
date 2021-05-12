/****************************************************************************
 * Copyright (c) 2017 ~ 2018.8 .

 ****************************************************************************/

namespace QFramework
{
    using UnityEngine;

    public enum UIMarkType
    {
        DefaultUnityElement,
        Element,
        Component
    }
    
    public interface IMark
    {
        string ComponentName { get; }
        
        string Comment { get; }

        Transform Transform { get; }

        UIMarkType GetUIMarkType();
    }
}