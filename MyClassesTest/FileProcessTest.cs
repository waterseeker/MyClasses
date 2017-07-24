using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System.Configuration; //we have to use this to read values from our system.config file. also had to add the system.config.dll 
using System.IO;
//in the references for MyClassesTest

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\BadFileName.bad"; //setting this as a const avoids hard coding it into the test method
        private string _GoodFileName; //initializes a var that we're going to assign the value of GOOD_FILE_NAME to from the config file. 

        #region Class Initialize and Cleanup THESE ONLY RUN ONCE, WHEN THE CLASS IS INSTANTIATED
        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            tc.WriteLine("In the Class Initialize.");
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
        }
        #endregion

        #region Test Initialize and Cleanup THESE RUN BEFORE/AFTER EVERY TEST
        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExist")) //checks to see if the name of the test begins with "FileNameDoesExist".
                SetGoodFileName();

            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine("Creating File: " + _GoodFileName);
                    File.AppendAllText(_GoodFileName, "Some Text"); //creates a file, but only when in the FileNameDoesExist test
                }
            }
        }
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExist"))
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine("Deleting File: " + _GoodFileName);
                    File.Delete(_GoodFileName); //deletes the file that we created for this test. 
                }
            }
        }
        #endregion

        public TestContext TestContext { get; set; } //the name HAS to be TestContext because that's what the unit test looks for
        //when they're going to create their instance of this text contest and pass it into your property

        [TestMethod]
        [Description("Check to see if a file does exist")] //you can put the description decorator either right before or right after the TestMethod. You can
        //use it to give a more detailed desciption of what the test does. Probably won't need to use this if you are using descriptive test names. 
        [Owner("Owner Name")] //you can make a note of who wrote the test here to make sure the right person gets notified if there is a problem.

        //you can group test by owner in the test explorer window by R clicking on a test, GROUP BY, TRAITS and then owner will be in the 
        //test explorer window and you'll be able to see who wrote what test. 

        [Priority(1)] //lets you set an arbitrary value that you can use to sort/execute by using the command line utility
        [TestCategory("Category4")] //lets you set an arbitrary value that you can use to sort/execute by using the command line utility

        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            //code below commented out when we put it in the TestInitialize code above
            //SetGoodFileName();
            //TestContext.WriteLine("Creating the file: " + _GoodFileName);
            //File.AppendAllText(_GoodFileName, "Some Text"); //creates a file
            TestContext.WriteLine("Testing the file: " + _GoodFileName);

            fromCall = fp.FileExists(_GoodFileName);
            //removed the code below when we added the system.config file, set a value in there to use in the SetGoodFileName method to avoid hard coding
            //fromCall = fp.FileExists(@"C:\Windows\Regedit.exe"); //this is just using a file we know to exist on the system.

            //code below commented out when we moved it to the TestCleanup
            //TestContext.WriteLine("Deleting the file: " + _GoodFileName);
            //File.Delete(_GoodFileName); //deletes the file that we created for this test. 
            Assert.IsTrue(fromCall); //asserts the response we're getting back is true
            //Assert.Inconclusive();
            //when you're setting up your tests, you can use the Assert.Inconclusive as a placeholder for a reminder that you need to write a test for this. 
        }
        [TestMethod]
        [Description("Check to see if a file does NOT exist")]
        [Owner("Owner Name")]
        [Priority(0)]
        [TestCategory("Category3")]

        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);
            //removed the code below to avoid hard coding. added it to the top of the file as a private constant
            //fromCall = fp.FileExists(@"C:\BadFileName.bad"); //we know this one does not exist

            Assert.IsFalse(fromCall); //asserts the response we're getting back is false
        }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]")) //checks to see if the var contains the "[AppPath]" token we gave it
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]", //if it does, it replaces the token with 
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)); //the ApplicationData path. This ensures the file exists 
                //no matter what environment this test is running on. 
            }
        }

        [TestMethod]
        [Description("Check to see if we get an ArbumentNullException if we pass in a null or empty file name")]
        [Owner("Owner Name")]
        [Priority(0)]
        [TestCategory("Category2")]

        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists(""); //passing it an empty string as the fileName
        }

        [TestMethod]
        [Description("Using try catch blocks, check to see if we get an ArbumentNullException if we pass in a null or empty file name")]
        [Owner("Owner Name")]
        [Priority(1)]
        [TestCategory("Category1")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                //the test was a success
                return;
            }

            Assert.Fail("Call to FileExists did not throw an ArgumentNull Exception");
        }

        [TestMethod]
        [Description("Ignore this test")]
        [Owner("Owner Name")]
        [Priority(0)]
        [TestCategory("Ignore")]
        [Ignore()] //lets you skip the test while still leaving it in your code. it'll show in your test list with a warning so you don't
        //forget about it. a good way to disable tests temporarily without having to comment them out/delete them from your tests. 
        [ExpectedException(typeof(ArgumentNullException))]
        public void IgnoreThisTest()
        {
           
        }

        [TestMethod()]
        [Owner("Timeout Owner")]
        [Timeout(3000)] //lets you set a timeout for the test in milliseconds that will make the test terminate after your timeout has passed. 
        public void SimulateTimeout()
        {
            System.Threading.Thread.Sleep(4000);//delays execution of this line by 4 seconds
        }

        private const string FILE_NAME = @"FileToDeploy.txt";

        [TestMethod()]
        [Owner("DeploymentItem Owner")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistUsingDeploymentItem()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = TestContext.DeploymentDirectory + @"\" + FILE_NAME;
            TestContext.WriteLine("Checking file: " + fileName);

            fromCall = fp.FileExists(fileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesExistSimpleMessage()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsFalse(fromCall, "This test is epected to fail because the file Does NOT exist."); //you can add a message as a second param to an assert so you'll 
            //get that message when the test fails
        }

        [TestMethod]
        public void FileNameDoesExistMessageWithFormatting()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsFalse(fromCall, "This test is expected to fail because the file '{0}' Does NOT exist.", _GoodFileName); //you can add a format string as an optional param
            //to an assert but you have to pass in whatever vars you use in it after the string in the assert call. 
        }
    }

}
