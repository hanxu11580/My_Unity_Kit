using UnityEngine;

namespace XhO_OKit
{

    public interface IPanel
    {
        CanvasGroup PanelCanvasGroup { get; set; }

        PanelState State { get; set; }

        void InitPanel();

        void OpenPanel();

        void ClosePanel();
    }
}