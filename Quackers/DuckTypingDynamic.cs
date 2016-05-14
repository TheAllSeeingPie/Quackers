using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quackers
{
    public class DuckTypingDynamic : DynamicObject
    {
        private readonly IDictionary<Type, object> _objectsInfo;
        public IEnumerable<object> Instances => _objectsInfo.Values;

        public DuckTypingDynamic(params object[] objects)
        {
            _objectsInfo = objects.ToDictionary(k => k.GetType(), v => v);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var key = _objectsInfo.Keys.SingleOrDefault(t => binder.ReturnType.IsAssignableFrom(t));
            if (key != null)
            {
                result = _objectsInfo[key];
                return true;
            }
            result = null;
            return false;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            foreach (var objectInfo in _objectsInfo)
            {
                try
                {
                    result = objectInfo.Key.InvokeMember(binder.Name, BindingFlags.InvokeMethod, null, objectInfo.Value, args);
                    return true;
                }
                catch (Exception)
                {
                }
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            foreach (var objectInfo in _objectsInfo)
            {
                try
                {
                    var property = objectInfo.Key.GetProperty(binder.Name);
                    result = property.GetValue(objectInfo.Value);
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return base.TryGetMember(binder, out result);
        }
    }

}
