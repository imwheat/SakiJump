//****************** 代码文件申明 ***********************
//* 文件：AndroidRotate
//* 作者：wheat
//* 创建时间：2025/02/11 15:52:19 星期二
//* 描述：
//*******************************************************

using UnityEngine;

namespace GameBuild
{
    public class AndroidRotate : MonoBehaviour
    {
#if UNITY_ANDROID

        private void Update()
        {
            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                Screen.orientation = ScreenOrientation.LandscapeRight;
            }
        }

#endif
    }
}

