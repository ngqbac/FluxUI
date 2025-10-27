using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FluxUI.Transition
{
    public abstract class UITransition: MonoBehaviour, IUITransition
    {
        public virtual bool IsPlaying { get; protected set; }
        public abstract UniTask PlayReveal();
        public abstract UniTask PlayConceal();
    }
}