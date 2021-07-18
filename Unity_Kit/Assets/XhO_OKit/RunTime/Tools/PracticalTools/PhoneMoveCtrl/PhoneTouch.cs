using UnityEngine;
using UnityEngine.Events;

namespace XhO_OKit
{
    public class PhoneTouch : MonoBehaviour
    {
        private Vector3 _downPos;
        private bool _down;

        [System.Serializable]
        public class OnPhoneDown : UnityEvent<Vector3> { }

        [System.Serializable]
        public class OnPhoneMove : UnityEvent<Vector3> { }

        /// <summary>
        /// 移动回调
        /// </summary>
        public OnPhoneDown onPhoneDown;

        /// <summary>
        /// 移动回调
        /// </summary>
        public OnPhoneMove onPhoneMove;

        private Vector2 UISize = default;


        //Vector2 Screen2Ugui(Vector3 scrrenPos)
        //{
        //    Vector2 screenpos2;
        //    screenpos2.x = scrrenPos.x - (Screen.width / 2);
        //    screenpos2.y = scrrenPos.y - (Screen.height / 2);
        //    Vector2 uipos;
        //    uipos.x = (screenpos2.x / Screen.width) * UISize.x;
        //    uipos.y = (screenpos2.y / Screen.height) * UISize.y;

        //    return uipos;
        //}

        public void PhoneUpdate()
        {

            if (Input.GetMouseButtonDown(0))
            {
                _down = true;
                _downPos = Input.mousePosition;
                onPhoneDown?.Invoke(_downPos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _down = false;
            }

            if (_down)
            {
                Vector3 mydir = Input.mousePosition - _downPos; //屏幕移动

                if (mydir.magnitude > 0)
                {
                    _downPos = Vector3.Lerp(_downPos, Input.mousePosition, 1);
                }

                onPhoneMove?.Invoke(mydir);
            }
            else
            {
                onPhoneMove?.Invoke(Vector3.zero);
            }
        }
    }
}