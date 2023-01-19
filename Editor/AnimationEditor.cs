using UnityEditor;
using Animation = Gamekit2D.Runtime.Actor.Animation;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Animation))]
    public class AnimationEditor : UnityEditor.Editor
    {
        private Animation x;

        public override void OnInspectorGUI() { }
    }
}