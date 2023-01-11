using UnityEngine;

namespace Gamekit2D.Runtime.Interactables.Power
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class PowerSupply : Interactable
    {
        [Header("Power")]
        [Tooltip("The list of targets this power supply actives")]
        [SerializeField] private Powered[] targets;
        [Tooltip("On/Off state for this power source")]
        [SerializeField] protected bool isOn;
        [Header("States")] 
        [Tooltip("Sprite to display when source is on")]
        [SerializeField] private Sprite onSprite;
        [Tooltip("Sprite to display when source is off")]
        [SerializeField] private Sprite offSprite;
        private SpriteRenderer sprite;
        
        public virtual bool IsOn
        {
            get => isOn;
            protected set
            {
                isOn = value; 
                RefreshSprite();
                EmitPower();
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if (targets is null || targets.Length < 1) return;
            DrawGizmo();
        }

        public void DrawGizmo()
        {
            if (targets.Length < 1) return;
            foreach (var target in targets)
            {
                if (target is null) continue;
                target.powerSupply = this;
                Gizmos.color = isOn ? new Color(.1f, 1f, 1f) : new Color(1,.7f, 0.1f);
                Gizmos.DrawLine(transform.position, target.transform.position);
                Gizmos.DrawWireCube(transform.position, Vector3.one);
                target.DrawGizmo();
            }
        }

        private void Awake() => Init();

        protected virtual void Init()
        {
            sprite = GetComponent<SpriteRenderer>();
            RefreshSprite();
        }

        protected void RefreshSprite() => sprite.sprite = isOn ? onSprite : offSprite;

        protected void EmitPower()
        {
            if (targets.Length < 1) return;
            foreach (var target in targets)
                target.UpdatePower();
        }
    }
}