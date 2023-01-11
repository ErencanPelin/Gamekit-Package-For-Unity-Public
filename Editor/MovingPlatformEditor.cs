using Gamekit2D.Runtime.LevelTiles;
using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CustomEditor(typeof(MovingPlatform))]
    public class MovingPlatformEditor : UnityEditor.Editor
    {
        private MovingPlatform x;

        private void OnEnable()
        {
            x = target as MovingPlatform;
        }

        public override void OnInspectorGUI() => DrawDefaultInspector();

        private void OnSceneGUI()
        {
            if (x.points.Count is 0) return;
            x.points[0] = x.transform.position;
            Handles.color = new Color(0.2f, 0.46f, 1f);
            x.transform.position = Handles.FreeMoveHandle(x.transform.position, Quaternion.identity, 0.25f, Vector3.one,
                Handles.CircleHandleCap);
            for (var i = 0; i < x.points.Count; i++)
            { 
                Handles.color = Color.cyan;
                if (i + 1 < x.points.Count)
                    Handles.DrawDottedLine(x.points[i], x.points[i + 1], 5f);
                if (i > 0)
                {
                    Handles.color = new Color(0.2f, 0.46f, 1f);
                    x.points[i] = Handles.FreeMoveHandle(x.points[i], Quaternion.identity, 0.25f, Vector3.one,
                        Handles.CircleHandleCap);
                }

                //  x.points[i] = Handles.PositionHandle(x.points[i], Quaternion.identity);
                Handles.Label(x.points[i], i + "");
            }
        }
    }
}