using UnityEngine;

namespace XhO_OKit
{
    public static class AnimatorExtension
    {
       
        /// <summary>
        /// 控制动画帧，设置完动画速度为0
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="animName">动画名字</param>
        /// <param name="layer">层级</param>
        /// <param name="progress">设置动画进度0~1</param>
        public static void SetAnimatorFrame(this Animator anim,string animName, int layer, float progress)
        {
            if (anim != null)
            {
                anim.speed = 0;
                anim.Play(animName, layer, progress);
            }
            else
            {
                throw new XhO_OKitException("Animator Is Null");
            }
        }
    }
}