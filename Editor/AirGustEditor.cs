using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AreaEffector2D))]
    public class AirGustEditor : UnityEditor.Editor
    {
        private AreaEffector2D x;
        private void OnEnable()
        {
            x = target as AreaEffector2D;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }

        private void OnSceneGUI()
        {
            Handles.color = Color.cyan;
            Handles.ArrowHandleCap(0, (Vector2)x.transform.position + x.GetComponent<BoxCollider2D>().offset,
                Quaternion.Euler(new Vector3(-x.GetComponent<AreaEffector2D>().forceAngle,90, 0)), 1f, EventType.Repaint);
        }
    }
}