# Quackers - Duck typing for .net!

Another one of those "I've had a weird idea but I'm gonna write it anyway projects!". Ever wanted to mung a load of types together into the same instance without them actually inheriting from each other? Well now you can with Quackers, the dynamic ducktyping solution for .net!

## Usage:
```
dynamic instance = DuckTypeFactory.CreateInstance(1, DateTime.Now);
int castAsInt = instance; //1
instance.ToLongDateString(); //Whatever DateTime.Now.ToLongDateString() returns :)
```
I'm sure you're asking: "Yeah but, if you cast it to string, how to do then cast it to something else?". Well there's some methods for finding the original DuckType again:

```
dynamic instance = DuckTypeFactory.CreateInstance(new Something(), 1, DateTime.Now);
ISomething castAsString = instance;
dynamic foundInstance = DuckTypeFactory.FindInstance(castAsString);
int castAsInt = foundInstance; //1 - WOO!
```
Unfortunately due to the .net framework being "highly optimised" it means that you can't duck type strings. Sorry about that ...