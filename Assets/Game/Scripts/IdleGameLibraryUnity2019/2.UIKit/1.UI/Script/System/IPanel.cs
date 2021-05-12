
namespace QFramework {
    using UnityEngine;

    /// <summary>
    /// IUIPanel.
    /// </summary>
    public interface IPanel {
        Transform Transform { get; }

        UIPanelInfo PanelInfo { get; set; }

        void Init(IUIData uiData = null, bool isShowTween = false);

        void Open(IUIData uiData = null);

        void Show();

        void Hide();

        void Close(bool destroy = true);
    }
}