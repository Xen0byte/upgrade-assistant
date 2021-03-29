namespace Microsoft.DotNet.UpgradeAssistant.Abstractions.Tests
{
    using System;
    using Microsoft.DotNet.UpgradeAssistant.Abstractions.Tests.TestClasses;
    using Xunit;

    /// <summary>
    /// Test class for IsApplicableToFramework extension method.
    /// </summary>
    public class ApplicableNetFrameworkProjectAttributeTests
    {
        /// <summary>
        /// Testing the IsApplicableToFramework extension method.
        /// </summary>
        /// <param name="sample">Test data.</param>
        /// <param name="expected">Expected result.</param>
        [InlineData(typeof(NetFramework472), false)]
        [InlineData(typeof(NetFrameworkAll), false)]
        [InlineData(typeof(NetFrameworkNone), true)]
        [InlineData(typeof(NetFrameworkUnknown), false)]
        [Theory]
        public void HasApplicableFramework_CanFilterByNone(Type sample, bool expected)
        {
            // act
            var actual = sample.IsApplicableToFramework(NetFrameworkProject.None);

            // assert
            if (expected)
            {
                Assert.True(actual, $"Expected {sample.Name} to be true");
            }
            else
            {
                Assert.False(actual, $"Expected false for {sample.Name}");
            }
        }

        /// <summary>
        /// Testing the IsApplicableToFramework extension method.
        /// </summary>
        /// <param name="sample">Test data.</param>
        /// <param name="expected">Expected result.</param>
        [InlineData(typeof(NetFramework472), true)]
        [InlineData(typeof(NetFrameworkAll), false)]
        [InlineData(typeof(NetFrameworkNone), false)]
        [InlineData(typeof(NetFrameworkUnknown), true)]
        [Theory]
        public void HasApplicableFramework_CanFilterByNet472(Type sample, bool expected)
        {
            // act
            var actual = sample.IsApplicableToFramework(NetFrameworkProject.Net472);

            // assert
            if (expected)
            {
                Assert.True(actual, $"Expected {sample.Name} to be true");
            }
            else
            {
                Assert.False(actual, $"Expected false for {sample.Name}");
            }
        }

        /// <summary>
        /// Testing the IsApplicableToFramework extension method.
        /// </summary>
        /// <param name="sample">Test data.</param>
        /// <param name="expected">Expected result.</param>
        [InlineData(typeof(NetFramework472), true)]
        [InlineData(typeof(NetFrameworkAll), true)]
        [InlineData(typeof(NetFrameworkNone), false)]
        [InlineData(typeof(NetFrameworkUnknown), true)]
        [Theory]
        public void HasApplicableFramework_CanFilterByAll(Type sample, bool expected)
        {
            // act
            var actual = sample.IsApplicableToFramework(NetFrameworkProject.All);

            // assert
            if (expected)
            {
                Assert.True(actual, $"Expected {sample.Name} to be true");
            }
            else
            {
                Assert.False(actual, $"Expected false for {sample.Name}");
            }
        }
    }
}
