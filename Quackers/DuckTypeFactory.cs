using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Quackers
{
    public class DuckTypeFactory
    {
        private static IList<DuckTypingDynamic> _instances = new List<DuckTypingDynamic>();

        public static dynamic CreateInstance(params object[] objects)
        {
            var instance = new DuckTypingDynamic(objects);
            _instances.Add(instance);
            return instance;
        }

        public static dynamic FindInstance(object instance)
        {
            return _instances.FirstOrDefault(i => i.Instances.Any(inst => instance == inst));
        }
    }
}