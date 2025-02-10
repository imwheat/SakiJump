//****************** 代码文件申明 ***********************
//* 文件：CameraManager
//* 作者：wheat
//* 创建时间：2025/02/09 15:08:18 星期日
//* 描述：相机管理器
//*******************************************************

using System;
using GameBuild.Player;
using UnityEngine;

namespace GameBuild
{
    public class CameraManager : MonoBehaviour
    {
        private static CameraManager instance;
        public static CameraManager Instance => instance;

        #region 属性配置

        public Transform followTarget;
        [SerializeField]
        private float yOffset = 5f;
        [SerializeField]
        private float ySize = 10f;
        #endregion

        #region 生命周期

        private void Awake()
        {
            instance = this;
        }

        private void LateUpdate()
        {
            //如果目标为空就返回
            if(followTarget == null) return;
            
            //更新相机位置
            float yPos = followTarget.position.y + yOffset;
            int y = (int)(yPos / ySize);
            transform.position = new Vector3(0f, y * ySize, transform.position.z);
        }

        #endregion

    }
}

