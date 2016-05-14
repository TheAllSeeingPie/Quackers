namespace SafeTypes.Tests
{
    public class HelloWorld : IHelloWorld
    {
        public string Hello()
        {
            return "Hello world!";
        }

        public int Version {get { return 1; } }
    }
}