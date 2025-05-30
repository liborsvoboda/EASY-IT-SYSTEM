Array is a collection of similar data which is stored in continuous memory addresses. Array values can be fetched using index. 

Index starts from 0 to size-1.

Arrays can be one-dimensional, multi-dimensional in C++ language. The more popular and frequently used arrays are one-dimensional and two-dimensional arrays.

Arrays store the collection of data sequentially in memory and they must share same data type.

# How to declare an array?

### One dimentional Array:

```c
data-type array-name[size];
```

### Two dimensional array:

```c
data-type array-name[size][size];
```

## Examples

### One dimentional Array:

```c
int a[5];
```

### Two dimentional Array:

```c
int a[2][3];
int b[][3]; // is also valid
```

# How to initialize arrays

### One dimentional Array:

```c
int a[5] = {1,2,3,4,5};
```

### Two dimentional Array:

```c
int a[2][3] = {
                {1,2,3},
                {4,5,6}
              };
```

# How to access array elements

Array elements can be accessed by using indices. Array indices starts from `0`.  `Array[n-1]` can be used to access nth element of an array.

## Examples

### One dimentional array:

```c
#include <iostream>
using namespace std;

int main() 
{
    int arr[5] = {1,2,3,4,5};
    int n = sizeof(arr)/sizeof(arr[0]); // gives length of an array
  cout << "Length of the array:" << n << endl;
  cout << "first element: " << arr[0] << endl; // prints first element of the array
  cout << "last element: " << arr[n-1]; // prints last element of the array
  return 0;
  
}

```
### Check result [here](https://onecompiler.com/cpp/3vmbf4ed9)

### Two dimentional array:

```c
#include <iostream>
using namespace std;

int main() 
{
  int a[2][3] = {
                {1,2,3},
                {4,5,6}
              };
  for(int i=0; i<2; i++) // iterates for each row
  {
    for(int j=0; j<3; j++) // iterates for each column
    {
        cout << a[i][j] << " ";
    }
        cout << endl;
  }   
}
```
### Check result [here](https://onecompiler.com/cpp/3vmbfbbzg)

## Creating array Dynamincally:
Unlike C creating an array dynamically is very easy using C++. 
- It can be easily creted using 'new' keyword.
- After declaring a pointer we can easily put the size of the array using two square brackets and it will allocate that much space in the memory.
- After performing our desired operation we can free up the space using delete opeartoer.
```c
#include <iostream>
using namespace std;

int main() 
{
    int* p = NULL;
    int n;// size of array
    cin>>n;// taking the size as input
    p = new int[n];// allocating a n size array into memory
    for(int i = 0; i < n; i++)
    {
      cin>>p[i];//putting elements in the dynamically created array
    }
    // Now we can perform any operation we want
    for(int i = 0; i < n; i++)
    {
      cout<<"Element at index"<<i<<" is : "<<p[i]<<endl;
    }
    delete []p;//using delete followed by square brackets to free the allocated memory
    p = NULL;//pointing the pointer back to null
    return 0;
}
```
### Check result [here](https://onecompiler.com/cpp/3ykh2m977)

## Summary

* Arrays is a collection of homogeneous data.
* Arrays stores data sequentially in memory.
* Arrays are finite.

