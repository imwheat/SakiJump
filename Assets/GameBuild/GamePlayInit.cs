//****************** 代码文件申明 ***********************
//* 文件：GamePlayInit
//* 作者：wheat
//* 创建时间：2025/02/10 14:01:45 星期一
//* 描述：游戏初始化
//*******************************************************

using System;
using GameBuild.Player;
using KFrame.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameBuild
{
    public class GamePlayInit : MonoBehaviour
    {
        [SerializeField, LabelText("玩家预制体")]
        private string playerPrefabKey = "Player";
        [SerializeField, LabelText("玩家起始位置")]
        private Vector2 initPos = new Vector2(0, -3.5f);

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (GameSaveManager.CurPlayData == null)
            {
                GameSaveManager.CurPlayData = new PlayData
                {
                    playerPos = initPos
                };
            }

            //生成玩家到指定位置
            GameObject player = PoolSystem.GetOrNewGameObject(playerPrefabKey);
            player.transform.position = GameSaveManager.CurPlayData.playerPos;
            CameraManager.Instance.followTarget = player.transform;
        }
    }
}

