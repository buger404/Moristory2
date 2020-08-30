//#define DEBUG_RENDER

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System;
using UnityEngine.Rendering.PostProcessing;

namespace UB.Simple2dWeatherEffects.LWRP
{
    [Serializable]
    [PostProcess(typeof(D2FogsPESecondRenderer), PostProcessEvent.AfterStack, "UB/Simple2dWeatherEffects/LWRP/D2FogsPESecond")]
    public sealed class D2FogsPESecond : PostProcessEffectSettings
    {
        public FloatParameter CameraSpeedMultiplier = new FloatParameter { value = 1f };
        public ColorParameter Color = new ColorParameter { value = new Color(1f, 1f, 1f, 1f) };
        public FloatParameter Size = new FloatParameter { value = 1f };
        public FloatParameter HorizontalSpeed = new FloatParameter { value = 0.2f };
        public FloatParameter VerticalSpeed = new FloatParameter { value = 0f };
        [Range(0.0f,5)]
        public FloatParameter Density = new FloatParameter { value = 2f };
        //public bool DarkMode = false;
        //public float DarkMultiplier = 1f;

        //public Shader Shader;
        //private Material _material;
    }

    public sealed class D2FogsPESecondRenderer : PostProcessEffectRenderer<D2FogsPESecond>
    {
        private bool _firstRun = false;
        private Vector3 _firstPosition;
        private Vector3 _difference;

        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/UB/Simple2dWeatherEffects/LWRP/D2Fogs"));
            //var sheet = context.propertySheets.Get(settings.Shader);

            if (!_firstRun)
            {
                _firstRun = true;
                _firstPosition = Camera.main.transform.position;
            }
            _difference = Camera.main.transform.position - _firstPosition;

            sheet.properties.SetColor("_Color", settings.Color);
            sheet.properties.SetFloat("_Size", settings.Size);
            sheet.properties.SetFloat("_Speed", settings.HorizontalSpeed);
            sheet.properties.SetFloat("_VSpeed", settings.VerticalSpeed);
            sheet.properties.SetFloat("_Density", settings.Density);
            sheet.properties.SetFloat("_CameraSpeedMultiplier", settings.CameraSpeedMultiplier);
            sheet.properties.SetFloat("_UVChangeX", _difference.x);
            sheet.properties.SetFloat("_UVChangeY", _difference.y);

            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }

}