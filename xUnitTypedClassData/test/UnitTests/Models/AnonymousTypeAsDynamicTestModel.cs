using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class AnonymousTypeAsDynamicTestModel
    {
        [Theory, TypedClassData(typeof(AnonymousTypeData))]
        public void AnonymousTypeTest(dynamic dynamicData)
        {

        }
    }
}
