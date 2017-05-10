using System;
using Xunit;

namespace lets_code_ci_session_2.Tests
{
    public class UnitTest1
    {
        #region Public Methods

        [Fact]
        public void TestMethod1()
        {
            Exception ex = Record.Exception(() => lets_code_ci_session_2.Class1.Method1());
        }

        #endregion Public Methods
    }
}