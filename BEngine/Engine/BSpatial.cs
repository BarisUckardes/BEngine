using BEngine.Engine.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    public class BSpatial : BComponent
    {
        private BVector3 _targetPosition;
        private BVector3 _targetRotation;
        private BVector3 _targetScale;
        internal bool IsDirty { get; set; }

        public Matrix4x4 ModelMatrix { get; internal set; }

        public BSpatial()
        {
            IsDirty = true;
            Scale = new BVector3(1, 1, 1);
        }
        public BVector3 Position
        {
            get
            {
                return _targetPosition;
            }
            set
            {
                IsDirty = true;
                _targetPosition = value;
            }
        }

        public BVector3 Rotation
        {
            get
            {
                return _targetRotation;
            }
            set
            {
                IsDirty = true;
                _targetRotation = value;
            }
        }

        public BVector3 Scale
        {
            get
            {
                return _targetScale;
            }
            set
            {
                IsDirty = true;
                _targetScale = value;
            }
        }

       

    }
}
