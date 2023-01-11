using UnityEngine;

namespace Gamekit2D.Runtime.Actor
{
    /// <summary>
    /// Handles all effects on actors or objects within a scene which create/emit effects
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu(" Eren Kit/Actor/Effect")]
    public class Effects : MonoBehaviour
    {
        [SerializeField] private bool isGlobal = true;
        [SerializeField] private float lifeTime = 3f;
        
        /// <summary>
        /// Spawns a particle system and then destroys it after the set lifeTime. Can also be invoked from the inspector as a UnityEvent
        /// </summary>
        /// <param name="particles">ParticleSystem to spawn</param>
        public void Play(ParticleSystem particles)
        {
            var p = Instantiate(particles, transform.position, Quaternion.identity,
                (isGlobal) ? null : transform);
            Destroy(p.gameObject, lifeTime);
        }
    }
}