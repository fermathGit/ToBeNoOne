//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    
    public partial class UIMainPanel
    {
        
        public const string NAME = "UIMainPanel";
        
        [SerializeField()]
        public InputField InputFieldClone;
        
        [SerializeField()]
        public RectTransform Step1;
        
        [SerializeField()]
        public InputField InputField_Income;
        
        [SerializeField()]
        public InputField InputField_Rate;
        
        [SerializeField()]
        public Text IncomeInValue;
        
        [SerializeField()]
        public Text IncomeCoValue;
        
        [SerializeField()]
        public Button BtnGo1;
        
        [SerializeField()]
        public RectTransform Step2;
        
        [SerializeField()]
        public RectTransform Input_Investment;
        
        [SerializeField()]
        public RectTransform Input_Consumption;
        
        [SerializeField()]
        public RectTransform ResultInvestment;
        
        [SerializeField()]
        public RectTransform ResultConsumption;
        
        [SerializeField()]
        public RectTransform ResultAll;
        
        [SerializeField()]
        public Button BtnGo;
        
        [SerializeField()]
        public InputField InputFieldDate;
        
        [SerializeField()]
        public Button BtnSave;
        
        [SerializeField()]
        public Button BtnClear;
        
        [SerializeField()]
        public Button BtnHistory;
        
        [SerializeField()]
        public Button BtnHistoryLast;
        
        [SerializeField()]
        public Button BtnHistoryNext;
        
        [SerializeField()]
        public Button BtnNew;
        
        private UIMainPanelData mPrivateData = null;
        
        public UIMainPanelData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new UIMainPanelData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            InputFieldClone = null;
            Step1 = null;
            InputField_Income = null;
            InputField_Rate = null;
            IncomeInValue = null;
            IncomeCoValue = null;
            BtnGo1 = null;
            Step2 = null;
            Input_Investment = null;
            Input_Consumption = null;
            ResultInvestment = null;
            ResultConsumption = null;
            ResultAll = null;
            BtnGo = null;
            InputFieldDate = null;
            BtnSave = null;
            BtnClear = null;
            BtnHistory = null;
            BtnHistoryLast = null;
            BtnHistoryNext = null;
            BtnNew = null;
            mData = null;
        }
    }
}