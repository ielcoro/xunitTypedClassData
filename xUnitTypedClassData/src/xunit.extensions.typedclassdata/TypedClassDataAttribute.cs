using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xunit.Extensions
{
    public class TypedClassDataAttribute : ClassDataAttribute
    {
        public TypedClassDataAttribute(Type @class)
            : base(@class)
        {

        }

        public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo methodUnderTest, Type[] parameterTypes)
        {
            if (this.Class.GetInterfaces().Contains(typeof(IEnumerable<object[]>)))
                return base.GetData(methodUnderTest, parameterTypes);

            var classDataProvider = (IEnumerable<object>)Activator.CreateInstance(this.Class);

            ParameterInfo[] targetParameters = methodUnderTest.GetParameters();

            return classDataProvider.Select(o =>
                {
                    Type dataType = o.GetType();

                    if (parameterTypes.Length == 1 && parameterTypes.Single() == dataType || parameterTypes.Single() == typeof(object))
                        return new[] { o };

                    PropertyInfo[] properties = dataType.GetProperties();

                    return (from t in targetParameters
                            from p in properties
                            where t.Name.Equals(p.Name, StringComparison.CurrentCultureIgnoreCase) &&
                                  t.ParameterType == p.PropertyType
                            select p.GetValue(o)).ToArray();
                });
        }
    }
}
