using System.Collections.Generic;
using UnityEngine;

namespace FluxUI.Transition
{
    public class UITransitionController: MonoBehaviour
    {
        [SerializeField] private List<UITransition> transitions = new();

        public IReadOnlyList<IUITransition> Transitions => transitions;

        public void Play()
        {
            foreach (var transition in transitions)
            {
                transition.PlayReveal();
            }
        }
    }
}