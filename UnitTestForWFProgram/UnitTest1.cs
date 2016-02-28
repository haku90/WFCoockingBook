using System;
using System.Activities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestForWFProgram
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string correctOutputValue = "Test Message";
            var output = WorkflowInvoker.Invoke(new WorkflowForTest());
            Assert.AreEqual(correctOutputValue, output["OutMessage"]);
        }
    }
}
