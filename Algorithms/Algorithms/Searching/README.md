# Search 

## Binary Search

```csharp
// array have to be sorted
int[] array = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// index = 3 
int index = BinarySearch.Rank(4, array);

// index = -1 because key is not present
int index = BinarySearch.Rank(11, array);
```

### Tips:

* The array have to be sorted
* If the key is not present you will receive the value `-1`
