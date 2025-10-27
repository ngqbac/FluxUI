using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FluxUI.Transition
{
    public interface IUITransition
    {
        UniTask PlayReveal();
        UniTask PlayConceal();
    }
}