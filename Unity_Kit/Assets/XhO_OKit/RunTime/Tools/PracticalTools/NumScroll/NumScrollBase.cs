using UnityEngine;
using DG.Tweening;

namespace XhO_OKit
{
    public abstract class NumScrollBase : MonoBehaviour
    {
        protected Sequence _seqSelf;
        protected int _selfProgress; //当前进度

        private TweenCallback _scrollCompletedCallback;
    
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(TweenCallback scrollCompletedCallback = null)
        {
            _scrollCompletedCallback = scrollCompletedCallback;
        }

        /// <summary>
        /// 每次滚动开始前操作
        /// </summary>
        public virtual void OnOPen()
        {
            _selfProgress = 0;
        }

        public void StartUpdateProgress(int targetProgress, float allLife)
        {
            _seqSelf = DOTween.Sequence();
            DoTweenExtension.NumberScroll(
                _seqSelf,
                _selfProgress,
                targetProgress,
                allLife,
                OnceScrollCallback
            ).AppendCallback(_scrollCompletedCallback);
        }
    
        protected abstract void OnceScrollCallback(int val);
    }   
}

