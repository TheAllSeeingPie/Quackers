using System;
using System.Runtime.InteropServices;
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
            dynamic subject = new DuckTypingDynamic(new HelloWorld(), new GoodbyeWorld());
            IHelloWorld helloWorld = subject;
            Assert.AreEqual("Hello world!", helloWorld.Hello());
            IGoodbyeWorld goodbyeWorld = subject;
            Assert.AreEqual("Goodbye world!", goodbyeWorld.Goodbye());
        }

        [TestMethod]
        public void Can_ducktype_be_passed_as_one_type_then_ducktyped_to_another()
        {
            dynamic subject = DuckTypeFactory.CreateInstance(new HelloWorld(), new GoodbyeWorld());
            IHelloWorld helloWorld = subject;
            Assert.AreEqual("Hello world!", helloWorld.Hello());

            IGoodbyeWorld goodbyeWorld = DuckTypeFactory.FindInstance(helloWorld);
            Assert.AreEqual("Goodbye world!", goodbyeWorld.Goodbye());
        }

        [TestMethod]
        public void Call_method_on_ducktype()
        {
            dynamic subject = new DuckTypingDynamic(new HelloWorld(), new GoodbyeWorld());
            Assert.AreEqual("Hello world!", subject.Hello());
            Assert.AreEqual(1, subject.Version);
            Assert.AreEqual("Goodbye world!", subject.Goodbye());
        }

        [TestMethod]
        public void Create_two_instances_and_hope_that_string_equality_doesnt_trump_object_equality()
        {
            dynamic subject = DuckTypeFactory.CreateInstance(new HelloWorld(), new GoodbyeWorld(), new A());
            dynamic subject2 = DuckTypeFactory.CreateInstance(new HelloWorld(), new GoodbyeWorld(), new B());

            HelloWorld helloWorld = subject;
            HelloWorld helloWorld2 = subject2;
            dynamic findInstance = DuckTypeFactory.FindInstance(helloWorld);
            dynamic findInstance2 = DuckTypeFactory.FindInstance(helloWorld2);
            Assert.AreNotEqual(findInstance, findInstance2);
            A a = findInstance;
            B b = findInstance2;
            Assert.IsInstanceOfType(a, typeof(A));
            Assert.IsInstanceOfType(b, typeof(B));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Cant_create_instance_of_DuckTypingDynamic_with_string()
        {
            new DuckTypingDynamic("Hello world!");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Cant_create_instance_of_DuckTypingDynamic_with_struct()
        {
            new DuckTypingDynamic(0);
        }

        public class A { }
        public class B { }

        [TestMethod]
        public void Test_weak_references()
        {
            var h1 = 1;
            var h2 = 2;

            var wr1 = GCHandle.Alloc(h1, GCHandleType.Pinned);
            var wr2 = GCHandle.Alloc(h2, GCHandleType.Pinned);

            Assert.AreNotEqual(wr1.AddrOfPinnedObject(), wr2.AddrOfPinnedObject());
        }

        public int PassThrough(int i)
        {
            return i;
        }
        
        public IHelloWorld PassThroughIHelloWorld(IHelloWorld helloWorld)
        {
            return helloWorld;
        }
    }
}
