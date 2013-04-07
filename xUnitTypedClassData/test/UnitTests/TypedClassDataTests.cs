using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Models;
using Xunit;
using Xunit.Sdk;

namespace UnitTests
{
    public class TypedClassDataTests
    {
        private IEnumerable<MethodResult> RunClass(Type typeUnderTest)
        {
            ITestClassCommand testClassCommand = new TestClassCommand(typeUnderTest);

            ClassResult classResult = TestClassCommandRunner.Execute(testClassCommand, testClassCommand.EnumerateTestMethods().ToList(),
                                                                     startCallback: null, resultCallback: null);

            return classResult.Results.OfType<MethodResult>();
        }

        [Fact]
        public void ShouldBeCompatibleWithObjectArrayEnumerables()
        {
            MethodResult testResult = RunClass(typeof(EnumerableObjectArrayTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.EnumerableObjectArrayTestModel.EnumerableTest(foo: 1, bar: ""bar"")", testResult.DisplayName);
        }

        [Fact]
        public void ShouldBeAbleToInjectAnonymousTypeAsObject()
        {
            MethodResult testResult = RunClass(typeof(AnonymousTypeAsObjectTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.AnonymousTypeAsObjectTestModel.AnonymousTypeTest(anonymousData: { Foo = 1, Bar = bar })", testResult.DisplayName);
        }

        [Fact]
        public void ShouldBeAbleToInjectAnonymousTypeAsDynamic()
        {
            MethodResult testResult = RunClass(typeof(AnonymousTypeAsDynamicTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.AnonymousTypeAsDynamicTestModel.AnonymousTypeTest(dynamicData: { Foo = 1, Bar = bar })", testResult.DisplayName);
        }

        [Fact]
        public void ShouldBeAbleToInjectAnonymousTypeAsParameters()
        {
            MethodResult testResult = RunClass(typeof(AnonymousTypeAsParametersTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.AnonymousTypeAsParametersTestModel.AnonymousTypeTest(foo: 1, bar: ""bar"")", testResult.DisplayName);
        }

    }
}
