using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClassesTest
{
    /// <summary>
    /// Assembly Initalize and cleanup methods
    /// </summary>
    [TestClass]
    public class MyClassesTestInitialization
    {
        public MyClassesTestInitialization()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            tc.WriteLine("In the assembly initilize method");
            //this is where you would create resources needed for your tests.
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            //this is where you would clean up any resources you created for your tests.
        }
    }
}
