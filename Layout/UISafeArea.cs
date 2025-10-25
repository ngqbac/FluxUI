using System;
using UnityEngine;

namespace FluxUI.Layout
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class UISafeArea : MonoBehaviour
    {
        [Flags]
        public enum SafeAreaEdges
        {
            None = 0,
            Top = 1 << 0,
            Bottom = 1 << 1,
            Left = 1 << 2,
            Right = 1 << 3,
            All = Top | Bottom | Left | Right
        }

        [SerializeField] private SafeAreaEdges safeEdges = SafeAreaEdges.All;
        [SerializeField] private bool logging;

        private RectTransform _rectTransform;
        private Rect _lastSafeArea;

        public SafeAreaPadding Padding { get; private set; } = new();
        public RectTransform RectTransform => _rectTransform;
        public Rect LastSafeArea => _lastSafeArea;

        private void Awake()
        {
            ApplySafeArea();
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.update += OnEditorUpdate;
#endif
            ApplySafeArea();
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.update -= OnEditorUpdate;
#endif
        }

        private void OnEditorUpdate()
        {
            if (!Application.isPlaying)
                ApplySafeArea();
        }

        private void OnRectTransformDimensionsChange()
        {
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            _rectTransform ??= GetComponent<RectTransform>();
            if (_rectTransform == null) return;

            var safeArea = Screen.safeArea;
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;

            _lastSafeArea = safeArea;

            Padding = new SafeAreaPadding
            {
                left = safeArea.x,
                bottom = safeArea.y,
                right = screenWidth - (safeArea.x + safeArea.width),
                top = screenHeight - (safeArea.y + safeArea.height)
            };

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= screenWidth;
            anchorMin.y /= screenHeight;
            anchorMax.x /= screenWidth;
            anchorMax.y /= screenHeight;

            var newAnchorMin = _rectTransform.anchorMin;
            var newAnchorMax = _rectTransform.anchorMax;

            if (safeEdges.HasFlag(SafeAreaEdges.Left)) newAnchorMin.x = anchorMin.x;
            if (safeEdges.HasFlag(SafeAreaEdges.Bottom)) newAnchorMin.y = anchorMin.y;
            if (safeEdges.HasFlag(SafeAreaEdges.Right)) newAnchorMax.x = anchorMax.x;
            if (safeEdges.HasFlag(SafeAreaEdges.Top)) newAnchorMax.y = anchorMax.y;

            _rectTransform.anchorMin = newAnchorMin;
            _rectTransform.anchorMax = newAnchorMax;

            if (logging) Debug.Log($"[UISafeArea] Applied anchors {newAnchorMin} - {newAnchorMax} (Padding: {Padding})");
        }
    }

    [Serializable]
    public class SafeAreaPadding
    {
        public float left;
        public float right;
        public float top;
        public float bottom;

        public override string ToString() => $"L:{left} R:{right} T:{top} B:{bottom}";
    }

    public static class UISafeAreaExtensions
    {
        public static void ModifySizeDelta(this UISafeArea safeArea, RectTransform target, Func<Vector2, SafeAreaPadding, Vector2> modify)
        {
            target.sizeDelta = modify(target.sizeDelta, safeArea.Padding);
        }

        public static void ModifyAnchoredPosition(this UISafeArea safeArea, RectTransform target, Func<Vector2, SafeAreaPadding, Vector2> modify)
        {
            target.anchoredPosition = modify(target.anchoredPosition, safeArea.Padding);
        }

        public static void ModifyOffsetMin(this UISafeArea safeArea, RectTransform target, Func<Vector2, SafeAreaPadding, Vector2> modify)
        {
            target.offsetMin = modify(target.offsetMin, safeArea.Padding);
        }

        public static void ModifyOffsetMax(this UISafeArea safeArea, RectTransform target, Func<Vector2, SafeAreaPadding, Vector2> modify)
        {
            target.offsetMax = modify(target.offsetMax, safeArea.Padding);
        }
    }
}