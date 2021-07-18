using DG.Tweening;
using System;
using UnityEngine;

namespace XhO_OKit
{
    public static class DoTweenExtension
    {
        /// <summary>
        /// 数字滚动拓展
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="oldVal">现在的数字数</param>
        /// <param name="targetVal">目标数字</param>
        /// <param name="lifeTime">所需时间</param>
        /// <param name="callback">滚动需要干的事（比如更新UITxt的值）// Ex: txt_Self.text = newVal.ToString() + "%";</param>
        public static Sequence NumberScroll(Sequence sequence, int oldVal, int targetVal, float lifeTime, Action<int> callback)
        {
            return sequence.Append(DOTween.To(() => oldVal, (int newVal) =>
            {
                callback.Invoke(newVal);
                oldVal = newVal;
            }, targetVal, lifeTime));
        }
        
        /// <summary>
        /// 生成一个按钮抖动的Dotween序列 返回DOTween.Sequence 可以自行调用播放和暂停
        /// </summary>
        /// <param name="target">抖动的目标</param>
        /// <param name="rotAng">单次抖动角度</param>
        /// <param name="rotDur">单次时长</param>
        /// <param name="scaleTo">扩大</param>
        /// <param name="isLoop">是否循环</param>
        /// <returns></returns>
        public static Sequence GenterateButtonShake(
            Transform target,
            float rotAng = 7f, float rotDur = 0.1f, float scaleTo = 1.1f, bool isLoop = false)
        {
            Sequence _seq = DOTween.Sequence();
            _seq.SetAutoKill(false);
            _seq.Append(target.DOScale(scaleTo, 0.3f).SetEase(Ease.Linear))
                .Append(target.DORotate(Vector3.back * rotAng, rotDur / 2f).SetEase(Ease.Linear))
                .Append(target.DORotate(Vector3.forward * rotAng, rotDur).SetEase(Ease.Linear))
                .Append(target.DORotate(Vector3.back * rotAng, rotDur).SetEase(Ease.Linear))
                .Append(target.DORotate(Vector3.forward * rotAng, rotDur).SetEase(Ease.Linear))
                .Append(target.DORotate(Vector3.back * rotAng, rotDur).SetEase(Ease.Linear))
                .Append(target.DORotate(Vector3.forward * rotAng, rotDur).SetEase(Ease.Linear))
                .Append(target.DORotate(Vector3.zero, rotDur / 2f).SetEase(Ease.Linear))
                .Append(target.DOScale(1f, 0.1f).SetEase(Ease.Linear))
                .AppendInterval(1f).SetLoops(isLoop ? -1 : 0);
            _seq.Pause();
            return _seq;
        }
        
    }
}
