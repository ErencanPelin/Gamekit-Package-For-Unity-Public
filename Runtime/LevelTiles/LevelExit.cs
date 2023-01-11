using Gamekit2D.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D.Runtime.LevelTiles
{
    [DisallowMultipleComponent]
    [AddComponentMenu(" Eren Kit/Level/Level Exit")]
    public class LevelExit : LevelTileBase
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private UnityEvent onPlayerEnter;

        protected override void Perform(GameObject interactingObject = null)
        {
            if (!interactingObject.CompareTag("Player")) return;
            onPlayerEnter.Invoke();
            if (!string.IsNullOrEmpty(sceneToLoad))
                Scenes.LoadScene(sceneToLoad);
        }
    }
}