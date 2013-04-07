using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class EnumerableObjectArrayTestModel
    {
        [Theory, ClassData(typeof(EnumerableObjectArrayData))]
        public void EnumerableTest(int foo, string bar)
        {

        }
    }

    public class EnumerableObjectArrayData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, "bar" };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
