//****************** 代码文件申明 ************************
//* 文件：IMovable                                       
//* 作者：wheat
//* 创建时间：2023/11/07 18:19:43 星期二
//* 描述：地图上一些可以根据设置移动的接口
//*****************************************************
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;

namespace GameBuild
{
    public interface IMovable
    {
        [SerializeField, LabelText("自动移动"), TabGroup("设置")]  bool autoMove { get; set; }
        [SerializeField, LabelText("机关移动"), TabGroup("设置")]  bool mechanismMove { get; set; }
        [SerializeField, LabelText("环形移动"), TabGroup("设置")]  bool circleMove { get; set; }
        [SerializeField, LabelText("偏移量"), TabGroup("设置")]  Vector2 offset { get; set; }
        [SerializeField, LabelText("移动速度"), TabGroup("设置")]  Vector2 velocity { get; set; }
        [SerializeField, LabelText("Transform"), TabGroup("设置")]  Transform transform { get;}
        [SerializeField, LabelText("停止时间"), TabGroup("设置")]  float StopTime { get; set; }
        [SerializeField, LabelText("移动速度"), TabGroup("设置")]  float Speed { get; set; }
        [SerializeField, LabelText("游荡设置位点"), TabGroup("设置")]  List<PatrolSet> PartolSets { get; set; }
        [SerializeField, LabelText("游荡位点"), ReadOnly, TabGroup("逻辑")]  List<GridCoordinate> partolPoses { get; set; }
        [SerializeField, LabelText("当前移动目标"), ReadOnly, TabGroup("逻辑")]  int curTarget { get; set; }
        [SerializeField, LabelText("当前位置"), ReadOnly, TabGroup("逻辑")]  int curIndex { get; set; }
        [SerializeField, LabelText("当前移动方向"), ReadOnly, TabGroup("逻辑")]  int dir { get; set; }
        [SerializeField, LabelText("停靠"), ReadOnly, TabGroup("逻辑")]  bool stop { get; set; }
        [SerializeField, LabelText("抵达"), ReadOnly, TabGroup("逻辑")]  bool arrive { get; set; }
        [SerializeField, LabelText("停止时间计时器"), ReadOnly, TabGroup("逻辑")] float stopTimer { get; set; }
        /// <summary>
        /// 到站后执行
        /// </summary>
        protected abstract void OnArriveTarget();
        /// <summary>
        /// 停止后执行
        /// </summary>
        protected abstract void OnMoveStop();
        /// <summary>
        /// 移动中执行
        /// </summary>
        protected abstract void OnMoving();
        /// <summary>
        /// 启动的时候执行
        /// </summary>
        protected abstract void OnStartMove();
        public void OnAwake()
        {
            //初始化参数
            curIndex = 0;
            curTarget = 1;
            dir = 1;
            velocity = Vector2.zero;
            Vector2 tmpPos = transform.position;
            offset = new Vector2(transform.position.x-tmpPos.x, transform.position.y-tmpPos.y);
            stop = true;
            arrive = true;
            partolPoses = new List<GridCoordinate>();

            if(PartolSets != null)
            {
                foreach (var p in PartolSets)
                {
                    partolPoses.Add((GridCoordinate)(p.transform.position - PartolSets[0].transform.position + transform.position));
                }
            }

        }
        public void OnFixedUpdate()
        {
            if (stop)
            {
                HandleStop();
            }
            else
            {
                HandleMove();
            }
        }
        private void HandleMove()
        {
            //判断一下是否到目的地了
            if (Vector3.Distance(transform.position, partolPoses[curTarget]+offset) <= 0.05f)
            {
                //到了就先把位置更新一下
                transform.position = partolPoses[curTarget]+offset;

                //更新ID
                curIndex = curTarget;
                //抵达
                arrive = true;

                //然后判定是否停靠
                if (PartolSets[curTarget].StopTime>0|| PartolSets[curTarget].StopPos)
                {
                    stopTimer = PartolSets[curTarget].StopTime;
                    if (PartolSets[curTarget].StopPos)
                    {
                        mechanismMove = false;
                    }
                    stop = true;

                    //停止运动
                    OnMoveStop();
                }

                //更新下一个目标点
                curTarget += dir;
                if(circleMove)
                {
                    //如果越界了那就回到0
                    if (curTarget >= partolPoses.Count)
                    {
                        curTarget = 0;
                    }
                }
                else
                {
                    //如果越界了那就要换向
                    if (curTarget >= partolPoses.Count)
                    {
                        dir *= -1;
                        curTarget -= 2;
                    }
                    else if (curTarget < 0)
                    {
                        dir *= -1;
                        curTarget += 2;
                    }
                }


                OnArriveTarget();
            }
            else
            {
                //如果没到就持续移动过去
                velocity = ((Vector2)(partolPoses[curTarget] - partolPoses[curIndex])).normalized * Speed;
                transform.position = Vector2.MoveTowards(transform.position, partolPoses[curTarget]+offset, Speed * Time.fixedDeltaTime);

                //移动过程中arrive为false
                arrive = false;
                
                OnMoving();
            }

        }

        /// <summary>
        /// 处理停靠
        /// </summary>
        private void HandleStop()
        {
            //如果抵达了
            if(arrive)
            {
                //倒计时要是归零了那就继续移动
                if (stopTimer > 0)
                {
                    stopTimer -= Time.fixedDeltaTime;
                }
                else if (autoMove||mechanismMove)
                {
                    stop = false;
                    arrive = false;

                    OnStartMove();
                }
            }
            //没有抵达说明半路停止移动了，那就返回起点
            else
            {
                //判断一下是否到目的地了
                if (Vector3.Distance(transform.position, partolPoses[curIndex] + offset) <= 0.05f)
                {
                    //到了就先把位置更新一下
                    transform.position = partolPoses[curIndex] + offset;

                    //抵达
                    arrive = true;

                    //然后停靠
                    stopTimer = StopTime;
                    stop = true;
                    velocity = Vector2.zero;

                    //停止运动
                    OnMoveStop();

                    OnArriveTarget();
                }
                else
                {
                    //如果没到就持续移动过去
                    velocity = ((Vector2)(partolPoses[curIndex] - partolPoses[curTarget])).normalized * Speed;
                    transform.position = Vector2.MoveTowards(transform.position, partolPoses[curIndex] + offset, Speed * Time.fixedDeltaTime);

                    OnMoving();
                }
            }
        }
    }
    
}

