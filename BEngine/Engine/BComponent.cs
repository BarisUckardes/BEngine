using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    public class BComponent : BObject
    {
        public virtual void Setup() { }
        public virtual void LogicUpdate() { }
    }
}
