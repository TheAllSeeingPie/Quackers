using System.Collections.Generic;
using System.Linq;

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
            return _instances.SingleOrDefault(i => i.Instances.Contains(instance));
        }
    }
}