using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions;

namespace UnitTests.Models
{
    public class IntegralTypeTestModel
    {
        [Theory, TypedClassData(typeof(IntegralTypeData))]
        public void ClassTypeTest(int data)
        {

        }
    }

    public class IntegralTypeData : IEnumerable<int>
    {

        public IEnumerator<int> GetEnumerator()
        {
            yield return 1;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
