using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    public class BObject
    {
        public string Name { get; set; }
        public Guid UniqueID { get; private set; }

        public BObject()
        {
            UniqueID = Guid.NewGuid();
        }
        public BObject BuildClone()
        {
            return this.MemberwiseClone() as BObject;
        }
    }
}
