using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class AnonymousTypeAsParametersTestModel
    {
        [Theory, TypedClassData(typeof(AnonymousTypeData))]
        public void AnonymousTypeTest(int foo, string bar)
        {

        }
    }
}
