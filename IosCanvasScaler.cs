using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIScripts
{
    [RequireComponent(typeof(Canvas))]
    [ExecuteAlways]
    [AddComponentMenu("Layout/iOS Canvas Scaler")]
    [DisallowMultipleComponent]
    public class IosCanvasScaler : UIBehaviour
    {
        public enum ScreenDpi
        {
            DPI132 = 132,
            DPI163 = 163,
            DPI264 = 264,
            DPI326 = 326,
            DPI338 = 338,
            DPI401 = 401,
            DPI458 = 458,
            DPI460 = 460,
            DPI476 = 476
        }

        private readonly Dictionary<ScreenDpi, int> _iosDpiConfigurations = new()
        {
            { ScreenDpi.DPI132, 1 },
            { ScreenDpi.DPI163, 1 },
            { ScreenDpi.DPI264, 2 },
            { ScreenDpi.DPI326, 2 },
            { ScreenDpi.DPI338, 2 },
            { ScreenDpi.DPI401, 3 },
            { ScreenDpi.DPI458, 3 },
            { ScreenDpi.DPI460, 3 },
            { ScreenDpi.DPI476, 3 },
        };

        [Header("iOS Device Display Reference:\n" +
                "https://www.ios-resolution.com")]
        [Space]

        [Tooltip("The DPI of the screen the UI is designed for.")]
        [SerializeField] protected ScreenDpi referenceDPI = ScreenDpi.DPI326;
        public ScreenDpi ReferenceDPI
        {
            get => referenceDPI;
            set => referenceDPI = value;
        }

        [Tooltip("The DPI to assume if the screen DPI is not known.")]
        [SerializeField] protected ScreenDpi fallbackDPI = ScreenDpi.DPI326;
        public ScreenDpi FallbackDPI
        {
            get => fallbackDPI;
            set => fallbackDPI = value;
        }

        [Tooltip("Simulate the use of the Fallback DPI instead of the Device Simulator's DPI.")]
        [SerializeField] protected bool simulateFallbackDPI;

        [Header("Sprite settings")]

        [Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
        [Min(1)]
        [SerializeField] protected float referencePixelsPerUnit = 100;
        public float ReferencePixelsPerUnit
        {
            get => referencePixelsPerUnit;
            set => referencePixelsPerUnit = Mathf.Max(1, value);
        }

        [Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
        [Min(1)]
        [SerializeField] protected float defaultSpriteDPI = (float)ScreenDpi.DPI326;
        public float DefaultSpriteDPI
        {
            get => defaultSpriteDPI;
            set => defaultSpriteDPI = Mathf.Min(1, value);
        }

        private Canvas _canvas;
        [System.NonSerialized]
        private float _prevScaleFactor = 1;
        [System.NonSerialized]
        private float _prevReferencePixelsPerUnit = 100;

        protected IosCanvasScaler() {}

        protected override void OnEnable()
        {
            base.OnEnable();
            _canvas = GetComponent<Canvas>();
            Handle();
            Canvas.preWillRenderCanvases += Handle;
        }

        protected override void OnDisable()
        {
            SetScaleFactor(1);
            SetReferencePixelsPerUnit(100);
            Canvas.preWillRenderCanvases -= Handle;
            base.OnDisable();
        }

        protected virtual void Handle()
        {
            if (_canvas == null || !_canvas.isRootCanvas || _canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                return;

            HandleConstantPhysicalSize();
        }

        protected virtual void HandleConstantPhysicalSize()
        {
            var currentDpi = Screen.dpi;
            var dpi = currentDpi == 0 || simulateFallbackDPI ? (float)fallbackDPI : currentDpi;
            var targetDPI = (float)referenceDPI / _iosDpiConfigurations[referenceDPI];

            SetScaleFactor(dpi / targetDPI);
            SetReferencePixelsPerUnit(referencePixelsPerUnit * targetDPI / defaultSpriteDPI);
        }

        private void SetScaleFactor(float scaleFactor)
        {
            if (scaleFactor == _prevScaleFactor)
                return;

            _canvas.scaleFactor = scaleFactor;
            _prevScaleFactor = scaleFactor;
        }

        private void SetReferencePixelsPerUnit(float pixelsPerUnit)
        {
            if (pixelsPerUnit == _prevReferencePixelsPerUnit)
                return;

            _canvas.referencePixelsPerUnit = pixelsPerUnit;
            _prevReferencePixelsPerUnit = pixelsPerUnit;
        }
    }
}