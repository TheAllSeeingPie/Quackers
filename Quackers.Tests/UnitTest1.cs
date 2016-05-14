using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quackers;

namespace SafeTypes.Tests
{
    [TestClass]
    public class DynamicsTests
    {
        [TestMethod]
        public void Can_a_dynamic_be_ducktyped()
        {
            dynamic subject = new DuckTypingDynamic(new HelloWorld(), "Hello world!");
            IHelloWorld helloWorld = subject;
            Assert.AreEqual("Hello world!", helloWorld.Hello());
            string s = subject;
            Assert.AreEqual("Hello world!", s);
        }

        [TestMethod]
        public void Can_ducktype_be_passed_as_one_type_then_ducktyped_to_another()
        {
            dynamic subject = DuckTypeFactory.CreateInstance(new HelloWorld(), "Hello world!");
            string s = subject;
            var newS = PassThrough(s);
            IHelloWorld helloWorld = DuckTypeFactory.FindInstance(newS);
            Assert.AreEqual("Hello world!", helloWorld.Hello());
            IHelloWorld helloWorldPassThrough = subject;
            var newIHelloWorld = PassThroughIHelloWorld(helloWorldPassThrough);
            string str = DuckTypeFactory.FindInstance(newIHelloWorld);
            Assert.AreEqual("Hello world!", str);
        }

        [TestMethod]
        public void Call_method_on_ducktype()
        {
            dynamic subject = new DuckTypingDynamic(new HelloWorld(), "Hello world!");
            Assert.AreEqual("Hello world!", subject.Hello());
            Assert.AreEqual(1, subject.Version);
        }


        public string PassThrough(string s)
        {
            return s;
        }
        
        public IHelloWorld PassThroughIHelloWorld(IHelloWorld helloWorld)
        {
            return helloWorld;
        }
    }


    public interface IHelloWorld
    {
        string Hello();
    }

    public class HelloWorld : IHelloWorld
    {
        public string Hello()
        {
            return "Hello world!";
        }

        public int Version {get { return 1; } }
    }
}
