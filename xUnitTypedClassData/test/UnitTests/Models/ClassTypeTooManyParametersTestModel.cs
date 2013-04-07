using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class ClassTypeTooManyParametersTestModel
    {
        [Theory, TypedClassData(typeof(ClassTypeData))]
        public void ClassTypeTest(int foo, string bar, int notDefined)
        {

        }
    }
}
