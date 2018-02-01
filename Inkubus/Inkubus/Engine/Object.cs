using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine
{
    using IO;

    public class Object : IDisposable
    {
        [BPD(0)]
        public string Name
        {
            get
            {
                return ObjectRegistry.instance.FirstOrDefault(x => x.Value == this).Key;
            }
            set
            {

                string n = Name;
                if (!string.IsNullOrEmpty(n))
                    ObjectRegistry.instance.Remove(Name);

                if (ObjectRegistry.instance.ContainsKey(value))
                    throw new Exception("ObjectRegistry: an object with that name already exists!");

                ObjectRegistry.instance.Add(value, this);

            }
        }

        public virtual void Dispose()
        {

        }
    }

    public class ObjectRegistry : Dictionary<string, Object>
    {
        public static ObjectRegistry instance;

        static ObjectRegistry()
        {
            if (instance == null)
                instance = new ObjectRegistry();
        }
    }
}
