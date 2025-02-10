//****************** 代码文件申明 ***********************
//* 文件：HoldButton
//* 作者：wheat
//* 创建时间：2025/02/10 20:43:01 星期一
//* 描述：按住控制的按钮
//*******************************************************

using System;
using KFrame.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameBuild
{
    public class HoldButton : KSelectable
    {
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        [Serializable]
        public class ButtonClickedEvent : UnityEvent {}
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();
        public ButtonClickedEvent OnClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }
        /// <summary>
        /// 按钮释放事件
        /// </summary>
        [SerializeField]
        private ButtonClickedEvent m_OnRelease = new ButtonClickedEvent();
        public ButtonClickedEvent OnRelease
        {
            get { return m_OnRelease; }
            set { m_OnRelease = value; }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            m_OnClick.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            
            m_OnRelease.Invoke();
        }
    }
}

