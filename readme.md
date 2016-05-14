# Quackers - Duck typing for .net!

Another one of those "I've had a weird idea but I'm gonna write it anyway projects!". Ever wanted to mung a load of types together into the same instance without them actually inheriting from each other? Well now you can with Quackers, the dynamic ducktyping solution for .net!

## Usage:
```
dynamic instance = DuckTypeFactory.CreateInstance("Hello world!", 1, DateTime.Now);
int castAsInt = instance; //1
string castAsString = instance; //"Hello world!"
instance.ToLongDateString(); //Whatever DateTime.Now.ToLongDateString() returns :)
```
I'm sure you're asking: "Yeah but, if you cast it to string, how to do then cast it to something else?". Well there's some methods for finding the original DuckType again:

```
dynamic instance = DuckTypeFactory.CreateInstance("Hello world!", 1, DateTime.Now);
string castAsString = instance; //"Hello world!"
dynamic foundInstance = DuckTypeFactory.FindInstance(castAsString);
int castAsInt = foundInstance; //1 - WOO!
```