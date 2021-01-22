using BEngine.Engine.Graphics;
using BEngine.Engine.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    public sealed class BSpectrumObserver : BComponent
    {
        internal static RenderingModule TargetRenderingModule {get;set;}

        public float _aspectRatio;
        public float _fieldOfView;
        public float _nearClipPlane;
        public float _farClipPlane;

        internal bool IsDirty { get; set; }

        public float AspectRatio
        {
            get
            {
                return _aspectRatio;
            }
            set
            {
                _aspectRatio = value;
                IsDirty = true;
            }
        }
        public float FieldOfView
        {
            get
            {
                return _fieldOfView;
            }
            set
            {
                _fieldOfView = value;
                IsDirty = true;
            }
        }
        public float NearClipPlane
        {
            get
            {
                return _nearClipPlane;
            }
            set
            {
                _nearClipPlane = value;
                IsDirty = true;
            }
        }
        public float FarClipPlane
        {
            get
            {
                return _farClipPlane;
            }
            set
            {
                _farClipPlane = value;
                IsDirty = true;
            }
        }
        public Matrix4x4 ViewMatrix { get; internal set; }
        public Matrix4x4 ProjectionMatrix { get; internal set; }

        public BRenderTexture2D RenderTarget { get; set; }

        public BSpectrumObserver()
        {
            AspectRatio = 1.0f;
            FieldOfView = 1.0f;
            NearClipPlane = 0.001f;
            FarClipPlane = 100000.0f;
            TargetRenderingModule.RegisterSpectrumObserver(this);
            RenderTarget = null;
        }


    }
}
