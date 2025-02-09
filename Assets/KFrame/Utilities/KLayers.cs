//****************** 代码文件申明 ***********************
//* 文件：Layers
//* 作者：wheat
//* 创建时间：2024/09/26 12:53:33 星期四
//* 描述：游戏中的Layer层级
//*******************************************************

using UnityEngine;

namespace KFrame.Utilities
{
    /// <summary>
    /// 游戏中的Layer层级
    /// </summary>
    public static class Layers
    {
        #region 代码生成开始标识

        /// <summary>
        /// 名称: Default
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly string DefaultLayer = "Default";
        /// <summary>
        /// 名称: Default
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly int DefaultLayerIndex = LayerMask.NameToLayer(DefaultLayer);
        /// <summary>
        /// 名称: Default
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly LayerMask DefaultLayerMask = LayerMask.GetMask(DefaultLayer);

        /// <summary>
        /// 名称: TransparentFX
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly string TransparentFXLayer = "TransparentFX";
        /// <summary>
        /// 名称: TransparentFX
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly int TransparentFXLayerIndex = LayerMask.NameToLayer(TransparentFXLayer);
        /// <summary>
        /// 名称: TransparentFX
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly LayerMask TransparentFXLayerMask = LayerMask.GetMask(TransparentFXLayer);

        /// <summary>
        /// 名称: Ignore Raycast
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly string IgnoreRaycastLayer = "Ignore Raycast";
        /// <summary>
        /// 名称: Ignore Raycast
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly int IgnoreRaycastLayerIndex = LayerMask.NameToLayer(IgnoreRaycastLayer);
        /// <summary>
        /// 名称: Ignore Raycast
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly LayerMask IgnoreRaycastLayerMask = LayerMask.GetMask(IgnoreRaycastLayer);

        /// <summary>
        /// 名称: Water
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly string WaterLayer = "Water";
        /// <summary>
        /// 名称: Water
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly int WaterLayerIndex = LayerMask.NameToLayer(WaterLayer);
        /// <summary>
        /// 名称: Water
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly LayerMask WaterLayerMask = LayerMask.GetMask(WaterLayer);

        /// <summary>
        /// 名称: UI
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly string UILayer = "UI";
        /// <summary>
        /// 名称: UI
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly int UILayerIndex = LayerMask.NameToLayer(UILayer);
        /// <summary>
        /// 名称: UI
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly LayerMask UILayerMask = LayerMask.GetMask(UILayer);

        /// <summary>
        /// 名称: Env
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly string EnvLayer = "Env";
        /// <summary>
        /// 名称: Env
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly int EnvLayerIndex = LayerMask.NameToLayer(EnvLayer);
        /// <summary>
        /// 名称: Env
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly LayerMask EnvLayerMask = LayerMask.GetMask(EnvLayer);

        /// <summary>
        /// 名称: Player
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly string PlayerLayer = "Player";
        /// <summary>
        /// 名称: Player
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly int PlayerLayerIndex = LayerMask.NameToLayer(PlayerLayer);
        /// <summary>
        /// 名称: Player
        /// 碰撞图层: Default, TransparentFX, Ignore Raycast, Water, UI, Env, Player, 
        /// </summary>
        public static readonly LayerMask PlayerLayerMask = LayerMask.GetMask(PlayerLayer);


        #endregion 代码生成结束标识
    }
}

