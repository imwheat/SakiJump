//****************** 代码文件申明 ***********************
//* 文件：PlayerController
//* 作者：wheat
//* 创建时间：2025/02/09 13:12:31 星期日
//* 描述：玩家控制器
//*******************************************************

using System;
using KFrame.Attributes;
using KFrame.Systems;
using KFrame.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameBuild.Player
{
    [DefaultExecutionOrder(-10)]
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance => _instance;
        
        #region 属性配置

        [TabGroup("基础配置")]
        public PlayerInputModule InputModule;
        [TabGroup("基础配置")]
        public PlayerInputModel InputModel;
        [TabGroup("基础配置")]
        public PlayerModel Model;

        [TabGroup("基础配置")]
        public PlayData PlayData
        {
            get => GameSaveManager.CurPlayData;
            set => GameSaveManager.CurPlayData = value;
        }
        /// <summary>
        /// 刚体
        /// </summary>
        [TabGroup("基础配置")]
        public Rigidbody2D Rb;
        /// <summary>
        /// 动画机
        /// </summary>
        [TabGroup("基础配置")]
        public Animator Anim;

        /// <summary>
        /// 跳跃音效id
        /// </summary>
        [SerializeField, LabelText("跳跃音效id"), AudioDescription, TabGroup("音效配置")]
        private int jumpAudioIndex;
        /// <summary>
        /// 落地音效id
        /// </summary>
        [SerializeField, LabelText("落地音效id"), AudioDescription, TabGroup("音效配置")]
        private int landAudioIndex;
        /// <summary>
        /// 撞墙音效id
        /// </summary>
        [SerializeField, LabelText("撞墙音效id"), AudioDescription, TabGroup("音效配置")]
        private int bumpAudioIndex;
        /// <summary>
        /// 砸地音效id
        /// </summary>
        [SerializeField, LabelText("砸地音效id"), AudioDescription, TabGroup("音效配置")]
        private int splatAudioIndex;

        /// <summary>
        /// 地面检测
        /// </summary>
        [SerializeField, LabelText("地面检测"),TabGroup("检测配置")]
        private Transform groundCheck;
        /// <summary>
        /// 地面检测矩形范围
        /// </summary>
        [SerializeField, LabelText("地面检测矩形范围"),TabGroup("检测配置")]
        private Vector2 groundCheckSize;
        /// <summary>
        /// 左侧墙检测
        /// </summary>
        [SerializeField, LabelText("左侧墙检测"),TabGroup("检测配置")]
        private Transform leftWallCheck;
        /// <summary>
        /// 右侧墙检测
        /// </summary>
        [SerializeField, LabelText("右侧墙检测"),TabGroup("检测配置")]
        private Transform rightWallCheck;
        /// <summary>
        /// 墙检测范围
        /// </summary>
        [SerializeField, LabelText("墙检测范围"),TabGroup("检测配置")]
        private Vector2 wallCheckSize;

        #endregion

        #region 生命周期

        private void Awake()
        {
            _instance = this;
            
            InputModule = GetComponent<PlayerInputModule>();
            InputModel = InputModule.InputModel;
            Model ??= new PlayerModel();
            PlayData ??= new PlayData();
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponentInChildren<Animator>();

            groundCheckSize = groundCheck.GetComponent<BoxCollider2D>().size;
            wallCheckSize = leftWallCheck.GetComponent<BoxCollider2D>().size;
        }

        private void Update()
        {
            //计时器处理
            HandleTimer();
            //处理输入
            HandleInput();
        }

        private void FixedUpdate()
        {
            PhysicsCheck();
        }

        private void OnDestroy()
        {
            //在删除的时候保存记录玩家位置
            if (PlayData != null)
            {
                PlayData.playerPos = transform.position;
            }
        }

        /// <summary>
        /// 处理计时器相关
        /// </summary>
        private void HandleTimer()
        {
            //游玩计时
            PlayData.playTime += Time.deltaTime;
            
            //倒地摔倒CD
            if (Model.SplatFreezeTimer > 0f)
            {
                Model.SplatFreezeTimer -= Time.deltaTime;
            }
        }

        #endregion

        #region 物理检测相关

        /// <summary>
        /// 物理检测相关
        /// </summary>
        private void PhysicsCheck()
        {
            bool onGround =
                Physics2D.OverlapBoxAll(groundCheck.transform.position, groundCheckSize, 0, Layers.EnvLayerMask)
                    .Length > 0;
            
            //判断是否在地面和落地
            if (!Model.OnJump)
            {
                if (!Model.OnGround && onGround)
                {
                    OnLand();    
                }
                
                Model.OnGround = onGround;
            }
            else if (onGround)
            {
                if (Rb.velocity.y > 0.1f)
                {
                    Model.OnGround = false;
                }
                else
                {
                    Model.OnGround = true;
                    OnLand();
                }
            }

            //掉落检测
            if (!Model.OnGround)
            {
                if (Rb.velocity.y - Model.RefVelocity.y < -0.1f)
                {
                    Model.FallingTimer += Time.deltaTime;
                }
                //以防一些特殊情况落地在边缘
                else if (Mathf.Abs(Rb.velocity.y - Model.RefVelocity.y) < 0.1f && Model.FallingTimer > 0.1f)
                {
                    Model.OnGround = true;
                    OnLand();
                }
            }
            
            //如果在跳跃中，检测是否撞到墙壁
            if (Model.OnJump && !onGround)
            {
                bool hitWall = false;
                
                if (Rb.velocity.x > 0.1f)
                {
                    hitWall =
                        Physics2D.OverlapBoxAll(rightWallCheck.transform.position, wallCheckSize, 0, Layers.EnvLayerMask)
                            .Length > 0;
                }
                else if (Rb.velocity.x < -0.1f)
                {
                    hitWall =
                        Physics2D.OverlapBoxAll(leftWallCheck.transform.position, wallCheckSize, 0, Layers.EnvLayerMask)
                            .Length > 0;
                }
                //防止特殊碰撞导致提前速度清零
                else if (Mathf.Abs(Model.JumpUpSpeed) > 0f && Mathf.Abs(Rb.velocity.y) > 0.1f)
                {
                    hitWall = true;
                }

                //如果撞到墙壁了那么速度反向
                if (hitWall)
                {
                    OnHitWall();
                }

            }

        }

        /// <summary>
        /// 撞到墙壁
        /// </summary>
        public void OnHitWall()
        {
            //如果速度大于0.1f的话那就更新速度
            if (Mathf.Abs(Rb.velocity.x - Model.RefVelocity.x) > 0.1f)
            {
                Model.JumpUpSpeed = Rb.velocity.x;
            }
            
            Model.JumpUpSpeed *= -1f;
            Rb.velocity = new Vector2(Model.JumpUpSpeed, Rb.velocity.y);
            Debug.Log($"[Player] 撞到墙壁了，反弹后的速度为 v = {Rb.velocity}");
            
            //播放音效
            AudioDic.PlayAudio(bumpAudioIndex, transform.position);
        }
        /// <summary>
        /// 落地
        /// </summary>
        public void OnLand()
        {
            //判断一下是否砸地
            if (Model.FallingTimer >= Model.SplatTimeThreshold)
            {
                //更新状态
                Model.SplatFreezeTimer = Model.SplatFreezeTime;
                PlayData.fallCount++;
                
                //播放音效
                AudioDic.PlayAudio(splatAudioIndex, transform.position);
            }
            //如果掉落时间非常小，那就不播放音效
            else if (Model.FallingTimer > 0.1f)
            {
                //播放音效
                AudioDic.PlayAudio(landAudioIndex, transform.position);
            }
            
            //重置状态
            Model.OnJump = false;
            Model.JumpUpSpeed = 0f;
            Model.FallingTimer = 0f;
        }

        #endregion

        #region 输入控制处理


        /// <summary>
        /// 输入控制处理
        /// </summary>
        private void HandleInput()
        {
            HandleMoveInput();
            HandleJumpInput();
        }
        /// <summary>
        /// 处理移动输入
        /// </summary>
        private void HandleMoveInput()
        {
            //砸地不能移动的冷却中
            if(Model.SplatFreezeTimer > 0f) return;
            
            
            //如果不在地面上或者按住跳跃键那就返回
            if (!Model.OnGround || InputModel.JumpInput)
            {
                //排除特殊情况处理
                if (Model.OnGround || Model.OnJump || !Mathf.Approximately(Rb.velocity.y, 0f))
                {
                    return;
                }
            }

            //更新速度
            Model.SelfVelocity = InputModel.MoveInput * Model.MoveSpeed;
            Rb.velocity = Model.RefVelocity + Model.SelfVelocity;
        }
        /// <summary>
        /// 更新参考系速度
        /// </summary>
        /// <param name="velocity"></param>
        public void UpdateRefVelocity(Vector2 velocity)
        {
            Model.RefVelocity = velocity;
            Rb.velocity = Model.SelfVelocity + Model.RefVelocity;
        }
        /// <summary>
        /// 处理跳跃输入
        /// </summary>
        private void HandleJumpInput()
        {
            //砸地不能移动的冷却中
            if(Model.SplatFreezeTimer > 0f) return;
            
            //如果不在地面那就返回
            if(Model.OnGround == false) return;

            //如果接收到跳跃输入
            if (InputModel.JumpInput)
            {
                //开始起跳判定
                if (!Model.StartJump)
                {
                    Model.StartJump = true;
                    //停止移动
                    Rb.velocity = Vector2.zero;
                }
                //计时跳跃输入
                if (Model.JumpPressTime < Model.JumpForceMaxThreshold)
                {
                    Model.JumpPressTime += Time.deltaTime;
                }
                //超过最大值那就直接跳跃
                else
                {
                    Jump();
                }
            }
            //如果在跳跃中松开了跳跃键，那就执行跳跃
            else if (Model.StartJump)
            {
                Jump();
            }
        }
        /// <summary>
        /// 跳跃
        /// </summary>
        private void Jump()
        {
            //获取跳跃力度
            float t = (Model.JumpPressTime - Model.JumpForceMinThreshold) / (Model.JumpForceMaxThreshold - Model.JumpForceMinThreshold);
            float jumpSpeed = Mathf.Lerp(Model.JumpForceLight, Model.JumpForceLarge, t);
            
            //重置参数
            Model.JumpPressTime = 0f;
            Model.StartJump = false;
            
            //判断水平速度
            float xSpeed = 0f;
            if (Mathf.Abs(InputModel.MoveInput.x) > 0.1f)
            {
                xSpeed = InputModel.MoveInput.x > 0 ? Model.JumpMoveSpeed : -Model.JumpMoveSpeed;
            }
            Model.JumpUpSpeed = xSpeed;

            //更新跳跃速度
            Rb.velocity = new Vector2(xSpeed, jumpSpeed);
            
            //更新状态
            Model.OnJump = true;
            Model.OnGround = false;
            PlayData.jumpCount++;
            
            //播放音效
            AudioDic.PlayAudio(jumpAudioIndex, transform.position);
        }

        #endregion

    }
}

