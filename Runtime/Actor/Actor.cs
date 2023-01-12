using UnityEngine;

namespace Gamekit.Runtime.Actors
{
  /// <summary>
  /// The main Actor component which all other components rely on
  /// </summary>
  [AddComponentMenu(" Eren Kit/Actor/Actor")]
  [DisallowMultipleComponent]
  [RequireComponent(typeof(SpriteRenderer))]
  [RequireComponent(typeof(Movement))]
  [RequireComponent(typeof(SpriteDirection))]
  public class Actor : MonoBehaviour
  {
    //Scripts are hidden as the gamekit is a commercial project
  }
}
