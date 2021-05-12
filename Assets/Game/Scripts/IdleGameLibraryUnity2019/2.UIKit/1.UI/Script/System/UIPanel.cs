
namespace QFramework {
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// 每个UIbehaviour对应的Data
    /// </summary>
    public interface IUIData {
    }

    public class UIPanelData : IUIData {
        protected UIPanel mPanel;
    }

    [Serializable]
    public class SubPanelInfo {
        public string PanelName;
        public UILevel Level;
    }

    public abstract class UIPanel : QMonoBehaviour, IPanel {
        [SerializeField] private List<SubPanelInfo> mSubPanelInfos = new List<SubPanelInfo>();

        public Transform Transform {
            get { return transform; }
        }

        public UIPanelInfo PanelInfo { get; set; }

        private IPanelLoader mPanelLoader = null;
        protected IUIData mUIData;

        public static UIPanel Load(string panelName, string assetBundleName = null) {
            var panelLoader = new DefaultPanelLoader();
            var panelPrefab = assetBundleName.IsNullOrEmpty()
                ? panelLoader.LoadPanelPrefab(panelName)
                : panelLoader.LoadPanelPrefab(assetBundleName, panelName);
            var obj = Instantiate(panelPrefab);
            var retScript = obj.GetComponent<UIPanel>();
            retScript.mPanelLoader = panelLoader;
            return retScript;
        }


        public override IManager Manager {
            get { return UIManager.Instance; }
        }

        protected override void OnBeforeDestroy() {
            ClearUIComponents();
        }

        protected virtual void ClearUIComponents() {
        }

        public void Init(IUIData uiData = null, bool isShowTween = false) {

            if (isShowTween) {
                var offset = transform.Find("Offset");
                if (offset != null) {
                    AnimSystem.Scale(offset.gameObject, Vector3.zero, Vector3.one, 0.2f, InterpType.Default, callBack: OnTweenCallBack);
                }
                else {
                    Debuger.LogWarning("Dont find Offset in openning panel with tween! ");
                }
            }

            mUIData = uiData;
            OnInit(uiData);
            InitUI(uiData);
            RegisterUIEvent();

            mSubPanelInfos.ForEach(subPanelInfo => UIMgr.OpenPanel(subPanelInfo.PanelName, subPanelInfo.Level));


        }

        public void Open(IUIData uiData = null) {
            OnOpen(uiData);
        }

        protected virtual void OnInit(IUIData uiData = null) {

        }

        protected virtual void OnOpen(IUIData uiData = null) {

        }

        /// <summary>
        /// avoid override in child class
        /// </summary>
        protected sealed override void OnDestroy() {
            base.OnDestroy();
        }

        /// <summary>
        /// 关闭,不允许子类调用
        /// </summary>
        void IPanel.Close(bool destroyed) {
            PanelInfo.UIData = mUIData;
            mOnClosed.InvokeGracefully();
            mOnClosed = null;

            OnClose();

            if (destroyed) {
                Destroy(gameObject);
            }

            mPanelLoader.Unload();
            mPanelLoader = null;
            mUIData = null;

            mSubPanelInfos.ForEach(subPanelInfo => UIMgr.ClosePanel(subPanelInfo.PanelName));
            mSubPanelInfos.Clear();
        }

        protected void CloseSelf() {
            UIManager.Instance.CloseUI(name);
        }

        protected void Back() {
            UIManager.Instance.Back(name);
        }

        /// <summary>
        /// 必须使用这个
        /// </summary>
        protected abstract void OnClose();

        private Action mOnClosed;

        public void OnClosed(Action onPanelClosed) {
            mOnClosed = onPanelClosed;
        }


        protected virtual void OnTweenCallBack(object[] arg) {

        }

        #region 不建议啊使用
        protected virtual void InitUI(IUIData uiData = null) {
        }

        protected virtual void RegisterUIEvent() {
        }

        #endregion
    }

    [Obsolete("弃用啦")]
    public abstract class QUIBehaviour : UIPanel {
    }
}