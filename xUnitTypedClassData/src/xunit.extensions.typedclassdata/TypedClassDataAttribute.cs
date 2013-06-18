using System;
using System.Collections;
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

            var classDataProvider = (IEnumerable)Activator.CreateInstance(this.Class);

            Func<object, object[]> projectionStrategy = null;

            return classDataProvider.Cast<object>().Select(data =>
                {
                    if (projectionStrategy == null)
                        projectionStrategy = DetermineProjectionStrategy(methodUnderTest, data, parameterTypes);

                    return projectionStrategy(data);
                });
        }

        private Func<object, object[]> DetermineProjectionStrategy(MethodInfo methodUnderTest, object sampleData, Type[] parameterTypes)
        {
            Func<object, object[]> projectionStrategy = null;

            Type dataType = sampleData.GetType();

            if (IsSingleParameterTargetStrategy(dataType, parameterTypes, out projectionStrategy))
                return projectionStrategy;

            return CreateParameterExpansionTargetStrategy(methodUnderTest, dataType);
        }

        private bool IsSingleParameterTargetStrategy(Type dataType, Type[] parameterTypes, out Func<object, object[]> strategy)
        {
            strategy = (data) => new[] { data };

            return parameterTypes.Length == 1 && 
                   (parameterTypes.Single() == dataType || parameterTypes.Single() == typeof(object));
        }

        private Func<object, object[]> CreateParameterExpansionTargetStrategy(MethodInfo methodUnderTest, Type dataType)
        {
            ParameterInfo[] targetParameters = methodUnderTest.GetParameters();
            PropertyInfo[] properties = dataType.GetProperties();

            return (data) => (from t in targetParameters
                                  from p in properties
                                  where t.Name.Equals(p.Name, StringComparison.CurrentCultureIgnoreCase) &&
                                        t.ParameterType == p.PropertyType
                                  select p.GetValue(data, null)).ToArray();
        }
    }
}
