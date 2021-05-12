/****************************************************************************
 * Copyright (c) 2017 ~ 2018.8 .
 * 
*
 *
*
*
 * 
*
 ****************************************************************************/

namespace QFramework
{
    using UnityEngine;

    /// <summary>
    /// belone to a panel 
    /// </summary>
    public abstract class UIElement : QMonoBehaviour,IMark
    {
        public virtual UIMarkType GetUIMarkType()
        {
            return UIMarkType.Element;
        }
        
        public abstract string ComponentName { get; }

        private string mCustomComent;

        public string Comment
        {
            get { return mCustomComent; }
        }

        public Transform Transform
        {
            get { return transform; }
        }

        public override IManager Manager
        {
            get { return UIManager.Instance; }
        }
    }
}