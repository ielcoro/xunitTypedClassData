using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class AnonymousTypeAsObjectTestModel
    {
        [Theory, TypedClassData(typeof(AnonymousTypeData))]
        public void AnonymousTypeTest(object anonymousData)
        {

        }
    }

    public class AnonymousTypeData : IEnumerable<object>
    {

        public IEnumerator<object> GetEnumerator()
        {
            yield return new { Foo = 1, Bar = "bar" };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
