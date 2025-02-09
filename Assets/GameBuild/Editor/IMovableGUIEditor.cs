//****************** 代码文件申明 ************************
//* 文件：IMovableGUIEditor                                       
//* 作者：wheat
//* 创建时间：2023/11/06 19:27:56 星期一
//* 描述：可以移动的机关的UI设置
//*****************************************************
using UnityEngine;
using GameBuild;

namespace UnityEditor
{
    [CustomEditor(typeof(IMovableGUI))]
    public class IMovableGUIEditor : Editor
    {
        /*不知道什么BUG有时候没反应，以后再说
        private void OnSceneGUI()
        {
            //先获取脚本
            IMovableGUI gui = null;

            //从当前选择物体获取
            GameObject obj = Selection.activeGameObject;
            //防空
            if (obj == null) return;

            gui = obj.GetComponent<IMovableGUI>();

            if (gui == null)
            {
                gui = obj.GetComponentInParent<IMovableGUI>();
            }

            //防空
            if (gui == null) return;

            if (gui.Movable == null) gui.Movable =gui.gameObject.GetComponent<IMovable>();
            IMovable moveable = gui.Movable;
            //获取不到就返回
            if (moveable == null) return;

            if (Application.isPlaying)
            {
                //绘制线段
                for (int i = 0; i < moveable.partolPoses.Count; i++)
                {
                    int link = (i + 1) % moveable.partolPoses.Count;

                    Handles.color = Color.red;

                    Handles.DrawLine(moveable.partolPoses[i]+moveable.offset, moveable.partolPoses[link] + moveable.offset);

                    Handles.Label(moveable.partolPoses[i], i.ToString(), new GUIStyle() { normal = new GUIStyleState() { textColor = Color.white } });
                }
            }
            else
            {
                //防空
                for (int i = 0; i < moveable.PartolSets.Count; i++)
                {
                    if (moveable.PartolSets[i] == null)
                    {
                        moveable.PartolSets.RemoveAt(i);
                        i--;
                    }
                }

                //绘制线段
                for (int i = 0; i < moveable.PartolSets.Count; i++)
                {
                    int link = (i + 1) % moveable.PartolSets.Count;

                    Handles.color = Color.red;

                    Debug.DrawLine(moveable.PartolSets[i].transform.position, moveable.PartolSets[link].transform.position, Color.red);

                    DrawBox(moveable.PartolSets[i].transform.position, Color.yellow);

                    Handles.Label(moveable.PartolSets[i].transform.position, i.ToString(), new GUIStyle() { normal = new GUIStyleState() { textColor = Color.white } });
                }
            }

        }
        private void DrawBox(Vector3 pos, Color color)
        {
            Handles.color = color;
            pos.x = Mathf.FloorToInt(pos.x) + 0.5f;
            pos.y = Mathf.FloorToInt(pos.y) + 0.5f;

            Handles.DrawWireCube(pos, new Vector3(1f, 1f));
        }
        */
        public override void OnInspectorGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;

            if(GUILayout.Button("添加新的移动位点",style))
            {
                IMovableGUI gui = (IMovableGUI)target;
                gui.AddNewPartolSet();
            }

            if (GUILayout.Button("清空所有的移动位点",style))
            {
                IMovableGUI gui = (IMovableGUI)target;
                gui.ClearAllPartolSet();
            }
        }
    }
}

