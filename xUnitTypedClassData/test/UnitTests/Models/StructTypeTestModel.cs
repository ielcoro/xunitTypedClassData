using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class StructTypeTestModel
    {
        [Theory, TypedClassData(typeof(StructTypeData))]
        public void ClassTypeTest(FooBarStruct classData)
        {

        }
    }

    public class StructTypeData : IEnumerable<FooBarStruct>
    {

        public IEnumerator<FooBarStruct> GetEnumerator()
        {
            yield return new FooBarStruct() { Foo = 1, Bar = "bar" };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public struct FooBarStruct
    {
        public int Foo { get; set; }
        public string Bar { get; set; }
    }
}
