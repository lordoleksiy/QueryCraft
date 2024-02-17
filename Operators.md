# QueryCraft Operators



QueryCraft supports a variety of operators that enable flexible and expressive filtering when constructing API requests. Here is a breakdown of supported operators along with examples of their usage:



## Equality (Eq)



```json

{

 "filter": {

  "field1": {

   "eq": "value1"

  }

 }

}

```



## Inequality (Ne)



```json

{

 "filter": {

  "field2": {

   "ne": "value2"

  }

 }

}

```



## Greater Than (Gt)



```json

{

 "filter": {

  "field3": {

   "gt": 10

  }

 }

}

```



## Less Than (Lt)



```json

{

 "filter": {

  "field4": {

   "lt": 20

  }

 }

}

```



## Greater Than or Equal To (Gte)



```json

{

 "filter": {

  "field5": {

   "gte": 100

  }

 }

}

```



## Less Than or Equal To (Lte)



```json

{

 "filter": {

  "field6": {

   "lte": 200

  }

 }

}

```



## In (In)



```json

{

 "filter": {

  "field7": {

   "in": ["value3", "value4", "value5"]

  }

 }

}

```



## Not In (Nin)



```json

{

 "filter": {

  "field8": {

   "nin": ["value6", "value7"]

  }

 }

}

```



## Starts With (StartsWith)



```json

{

 "filter": {

  "field11": {

   "startsWith": "abc"

  }

 }

}

```



## Ends With (EndsWith)



```json

{

 "filter": {

  "field12": {

   "endsWith": "xyz"

  }

 }

}

```



## Between (Between)



```json

{

 "filter": {

  "field13": {

   "between": [10, 20]

  }

 }

}

```



## Is Null (IsNull)



```json

{

 "filter": {

  "field15": {

   "isNull": true

  }

 }

}

```



## Logical AND (And)



```json

{

 "filter": {

  "and": [

   {

    "field1": {

     "eq": "value1"

    }

   },

   {

    "field2": {

     "gt": 10

    }

   }

  ]

 }

}

```



## Logical OR (Or)



```json

{

 "filter": {

  "or": [

   {

    "field3": {

     "lt": 5

    }

   },

   {

    "field4": {

     "gte": 100

    }

   }

  ]

 }

}

```



## Logical NOT (Not)



```json

{

 "filter": {

  "not": {

   "field5": {

    "eq": "value2"

   }

  }

 }

}

```

These operators provide powerful capabilities for constructing dynamic and sophisticated queries in your API requests using the QueryCraft library.

# Examples of right request bodies:

1. Using the logical AND operator:

```json
"filter": {
  "and": [
    {
      "field1": {
        "between": [10, 100]
      }
    },
    {
      "field2": {
        "eq": "Vendor"
      }
    },
    {
      "field3": {
        "is null": true
      }
    }
  ]
}
```

2. Using the logical OR operator with nested AND conditions:

```json
"filter": {
  "or": [
    {
      "and": [
        {
          "field4": {
            "is null": true
          }
        },
        {
          "field1": {
            "lt": 10
          }
        }
      ]
    },
    {
      "and": [
        {
          "field5": {
            "startswith": "O"
          }
        },
        {
          "field5": {
            "endswith": "l"
          }
        },
        {
          "field1": {
            "gt": 100
          }
        }
      ]
    }
  ]
}
```

3. Multiple conditions with various operators:

```json
"filter": {
  "field6": {
    "gt": "2021-01-01T00:00:00"
  },
  "field1": {
    "between": [100, 500]
  },
  "field2": {
    "eq": "Vendor"
  },
  "field3": {
    "is null": false
  },
  "field7": {
    "lt": "2025-01-01T00:00:00"
  }
}
```

4. Hybrid structure with both OR and nested AND conditions:

```json
"filter": {
  "or": [
    {
      "and": {
        "field6": {
          "gt": "2021-01-01T00:00:00"
        },
        "field1": {
          "between": [100, 500]
        },
        "field2": {
          "eq": "Vendor"
        },
        "field3": {
          "is null": false
        },
        "field7": {
          "lt": "2025-01-01T00:00:00"
        }
      }
    },
    {
      "and": [
        {
          "field5": {
            "startswith": "O"
          }
        },
        {
          "field5": {
            "endswith": "l"
          }
        },
        {
          "field1": {
            "gt": 100
          }
        }
      ]
    }
  ]
}
```

These examples now use generic field names (field1, field2, etc.) for better readability.


