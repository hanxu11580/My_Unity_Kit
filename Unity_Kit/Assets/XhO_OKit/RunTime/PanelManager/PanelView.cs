using System;
using UnityEngine;
using XhO_OKit.DataBinding;

namespace XhO_OKit
{
    /// <summary>
    /// 面板视图,继承IPanel。后面考虑聚合代替继承
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelView<T> : ViewBase<T>, IPanel where T : ViewModelBase
    {
        private bool _isInited;
        public CanvasGroup PanelCanvasGroup { get; set; }
        public PanelState State { get; set; }

        /// <summary>
        /// 绑定子View时覆写
        /// </summary>
        public virtual void InitPanel()
        {
            PanelCanvasGroup = transform.GetComponent<CanvasGroup>();
            BindingContext = Activator.CreateInstance<T>();
        }

        protected virtual void OnStartOpen() 
        {
            if (!_isInited)
            {
                BindingContext.OnInitialize();
                _isInited = true;
            }
        }

        protected virtual void OnStartClose() { }

        public void ClosePanel()
        {
            OnStartClose();
            PanelCanvasGroup.alpha = 0;
            gameObject.SetActive(false);
            State = PanelState.Opened;
        }

        public void OpenPanel()
        {
            OnStartOpen();
            gameObject.SetActive(true);
            PanelCanvasGroup.alpha = 1;
            State = PanelState.Closed;
        }
    }
}