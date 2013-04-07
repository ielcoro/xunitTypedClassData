using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class ClassTypeAsObjectTestModel
    {
        [Theory, TypedClassData(typeof(ClassTypeData))]
        public void ClassTypeTest(object classData)
        {

        }
    }

    public class ClassTypeData : IEnumerable<FooBar>
    {

        public IEnumerator<FooBar> GetEnumerator()
        {
            yield return new FooBar() { Foo = 1, Bar = "bar" };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class FooBar
    {
        public int Foo { get; set; }
        public string Bar { get; set; }
    }
}
