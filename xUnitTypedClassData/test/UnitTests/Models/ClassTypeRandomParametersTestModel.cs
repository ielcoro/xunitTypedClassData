using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class ClassTypeRandomParametersTestModel
    {
        [Theory, TypedClassData(typeof(ClassTypeData))]
        public void ClassTypeTest(string bar)
        {

        }
    }
}
