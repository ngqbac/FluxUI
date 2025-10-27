using Cysharp.Threading.Tasks;
using FluxUI.Binding;
using FluxUI.Logging;
using FluxUI.Transition;
using OrganicBeing.Integration.Async;
using OrganicBeing.Integration.Unity;
using UnityEngine;

namespace FluxUI.UIElements
{
    public abstract class UIElement : MonoOrganic, IUIElement
    {
        public bool IsVisible { get; private set; }

        public string ElementId { get; protected set; }

        public bool useFluxTransition;
        
        [SerializeField] private UITransitionController transitionController;
        
        public void Reveal() => RevealAsync().Forget();

        public void Conceal() => ConcealAsync().Forget();

        public virtual void SetDefault() { }

        public virtual async UniTask RevealAsync() => await this.WhenReadyAsync(() =>
        {
            OnWillReveal();
            gameObject.SetActive(true);
            IsVisible = true;
            if (useFluxTransition)
            {
                OnDidReveal();
            }
            else
            {
                if (transitionController != null)
                {
                    transitionController.Play();
                    OnDidReveal();
                }
                else
                {
                    FluxUILogger.Error("TransitionController not found");
                    OnDidReveal();
                }
            }
        });

        public virtual async UniTask ConcealAsync() => await this.WhenReadyAsync(() =>
        {
            OnWillConceal();
            gameObject.SetActive(false);
            IsVisible = false;
            OnDidConceal();
            SetDefault();
        });

        protected virtual void OnWillReveal()
        {
            OnWillRevealCommand.Invoke();
        }

        protected virtual void OnDidReveal()
        {
            OnDidRevealCommand.Invoke();
        }

        protected virtual void OnWillConceal()
        {
            OnWillConcealCommand.Invoke();
        }

        protected virtual void OnDidConceal()
        {
            OnDidConcealCommand.Invoke();
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            Conceal();
            OnWillRevealCommand.Clear();
            OnDidRevealCommand.Clear();
            OnWillConcealCommand.Clear();
            OnDidConcealCommand.Clear();
        }
        
        public readonly EventCommand OnWillRevealCommand = new();
        public readonly EventCommand OnDidRevealCommand = new();
        public readonly EventCommand OnWillConcealCommand = new();
        public readonly EventCommand OnDidConcealCommand = new();

        protected static void Verbose(object message) => FluxUILogger.Verbose(message);
        
#if UNITY_EDITOR
        private void __EditorAssignTransitionController(UITransitionController controller) => transitionController = controller;
#endif
    }
}