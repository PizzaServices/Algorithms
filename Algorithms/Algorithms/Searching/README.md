# Search 

## Binary Search

```csharp
// array have to be sorted
var list = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// index = 3 
int index = list.BSearch(4);

// index = -1 because element is not present
int index = list.BSearch(11);

// you can also pass an comparer to the method
int index = list.BSearch(5, yourComparer);
```

### Tips:

* The Collection has to be sorted
* If the element is not present you will receive the value `-1`
