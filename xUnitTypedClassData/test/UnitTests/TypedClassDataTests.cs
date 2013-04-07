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

        [Fact]
        public void ShouldBeAbleToInjectClassTypeAsObject()
        {
            MethodResult testResult = RunClass(typeof(ClassTypeAsObjectTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.ClassTypeAsObjectTestModel.ClassTypeTest(classData: UnitTests.Models.FooBar)", testResult.DisplayName);
        }

        [Fact]
        public void ShouldBeAbleToInjectClassTypeAsDynamic()
        {
            MethodResult testResult = RunClass(typeof(ClassTypeAsDynamicTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.ClassTypeAsDynamicTestModel.ClassTypeTest(dynamicData: UnitTests.Models.FooBar)", testResult.DisplayName);
        }

        [Fact]
        public void ShouldBeAbleToInjectClassTypeAsParameters()
        {
            MethodResult testResult = RunClass(typeof(ClassTypeAsParametersTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.ClassTypeAsParametersTestModel.ClassTypeTest(foo: 1, bar: ""bar"")", testResult.DisplayName);
        }

        [Fact]
        public void ShouldBeAbleToInjectClassTypeAsTypedInstance()
        {
            MethodResult testResult = RunClass(typeof(ClassTypeAsTypedInstanceTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.ClassTypeAsTypedInstanceTestModel.ClassTypeTest(data: UnitTests.Models.FooBar)", testResult.DisplayName);
        }

        [Fact]
        public void ShouldAllowCaseDiferencesOnParameterInjection()
        {
            MethodResult testResult = RunClass(typeof(ClassTypeAsParametersWithCaseDiferencesTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.ClassTypeAsParametersWithCaseDiferencesTestModel.ClassTypeTest(FOO: 1, bAr: ""bar"")", testResult.DisplayName);
        }

        [Fact]
        public void ShouldAllowInjectionOfRandomParameters()
        {
            MethodResult testResult = RunClass(typeof(ClassTypeRandomParametersTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.ClassTypeRandomParametersTestModel.ClassTypeTest(bar: ""bar"")", testResult.DisplayName);
        }

        [Fact]
        public void ShouldThrowWhenMoreParametersThanClassDefinitionAreFound()
        {
            MethodResult testResult = RunClass(typeof(ClassTypeTooManyParametersTestModel)).Single();
            FailedResult failedResult = (FailedResult)testResult;

            Assert.Equal(typeof(InvalidOperationException).FullName, failedResult.ExceptionType);
            Assert.Equal("System.InvalidOperationException : Expected 3 parameters, got 2 parameters", failedResult.Message);
        }

        [Fact]
        public void ShouldBeAbleToUseIntegralTypes()
        {
            MethodResult testResult = RunClass(typeof(IntegralTypeTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.IntegralTypeTestModel.ClassTypeTest(data: 1)", testResult.DisplayName);
        }

        [Fact]
        public void ShouldBeAbleToInjectStructs()
        {
            MethodResult testResult = RunClass(typeof(StructTypeTestModel)).Single();

            Assert.IsType<PassedResult>(testResult);
            Assert.Equal(@"UnitTests.Models.StructTypeTestModel.ClassTypeTest(classData: UnitTests.Models.FooBarStruct)", testResult.DisplayName);
        }
    }
}
