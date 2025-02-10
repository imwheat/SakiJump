//****************** 代码文件申明 ***********************
//* 文件：MoveMechanism
//* 作者：wheat
//* 创建时间：2025/02/09 20:36:16 星期日
//* 描述：移动机关
//*******************************************************

using System;
using System.Collections.Generic;
using GameBuild.Player;
using KFrame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameBuild
{
    public class MoveMechanism : MonoBehaviour, IMovable
    {

        #region 机关设置

        [LabelText("自动移动")]
        public bool AutoMove = true;
        [LabelText("环形移动")]
        public bool CircleMove = false;
        [LabelText("移动速度")]
        public float MoveSpeed = 3f;
        [LabelText("当前速度")]
        public Vector2 CurVelocity = Vector2.zero;

        [SerializeField]
        private List<PatrolSet> _partolSets = new();
        [SerializeField]
        private List<GridCoordinate> _partolPos = new();
        #endregion

        #region 逻辑检测

        /// <summary>
        /// 玩家
        /// </summary>
        [SerializeField]
        private PlayerController player;

        #endregion
        
        #region 接口配置

        public bool autoMove { get => AutoMove; set => AutoMove = value; }
        public bool mechanismMove { get; set; }
        public bool circleMove { get => CircleMove; set => CircleMove = value; }
        public Vector2 offset { get; set; }
        public Vector2 velocity { get=>CurVelocity; set=>CurVelocity = value; }
        public float StopTime { get; set; }
        public float Speed { get => MoveSpeed; set => MoveSpeed = value; }
        public List<PatrolSet> PartolSets { get => _partolSets; set => _partolSets = value; }
        public List<GridCoordinate> partolPoses { get=> _partolPos; set => _partolPos = value; }
        public int curTarget { get; set; }
        public int curIndex { get; set; }
        public int dir { get; set; }
        public bool stop { get; set; }
        public bool arrive { get; set; }
        public float stopTimer { get; set; }

        #endregion

        #region 生命周期

        private void Awake()
        {
            ((IMovable)this).OnAwake();
        }

        private void FixedUpdate()
        {
            ((IMovable)this).OnFixedUpdate();
        }

        #endregion
        
        #region 物理检测

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                PlayerController player = other.GetComponentInParent<PlayerController>();

                //如果获取到了玩家
                if (player != null)
                {
                    this.player = player;
                    MoveSpeedSync();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                PlayerController player = other.GetComponentInParent<PlayerController>();

                //如果获取到了玩家
                if (player != null && this.player != null)
                {
                    this.player.Model.RefVelocity = Vector2.zero;
                    this.player = null;
                }
            }
        }

        #endregion

        #region 接口方法实现

        void IMovable.OnArriveTarget()
        {
        }

        void IMovable.OnMoveStop()
        {
            velocity = Vector2.zero;
            MoveSpeedSync();
        }

        void IMovable.OnMoving()
        {
            MoveSpeedSync();
        }

        void IMovable.OnStartMove()
        {
            MoveSpeedSync();
        }

        /// <summary>
        /// 移动速度同步
        /// </summary>
        private void MoveSpeedSync()
        {
            //如果玩家不为空，并且不在跳跃，那么同步速度
            if (player != null)
            {
                if (!player.Model.OnJump)
                {
                    player.UpdateRefVelocity(velocity);
                }
            }
        }

        #endregion

    }
}

