# Quackers - Duck typing for .net!

Another one of those "I've had a weird idea but I'm gonna write it anyway projects!". Ever wanted to mung a load of types together into the same instance without them actually inheriting from each other? Well now you can with Quackers, the dynamic ducktyping solution for .net!

## Usage:
```
dynamic instance = DuckTypeFactory.CreateInstance(new HelloWorld(), new GoodbyeWorld());
IHelloWorld helloWorld = instance;
helloWorld.Hello(); //"Hello world!"
IGoodbyeWorld goodbyeWorld = instance;
goodbyeWorld.Goodbye(); //"Goodbye world!"
```
I'm sure you're asking: "Yeah but, if you cast it to string, how to do then cast it to something else?". Well there's some methods for finding the original DuckType again:

```
dynamic instance = DuckTypeFactory.CreateInstance(new Something(), new SomethingElse());
ISomething something = instance;
dynamic foundInstance = DuckTypeFactory.FindInstance(something);
SomethingElse somethingElse = foundInstance;
somethingElse.Speak(); //"Something else"
```
Unfortunately due to the .net framework being "highly optimised" it means that you can't duck type strings. Also because structs are passed by value they aren't able to be tracked back via reference. Sorry about that ...