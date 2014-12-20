JPath.Net
=========

A library which can handle JSON with XPath similar syntax

Example
========
```c#
var jsonObj = org.lmatt.JBase.ParseJson ("{\"key1\" : [], \"key2\": {}, \"key3\": [1, 2, 3], \"key4\": {\"key5\": \"value1\"}, \"key5\": true, \"key6\": 1}");
var results = jsonObj.JPathSelects ("//key5|//key6"); //Get a list of result
var result = jsonObj.JPathSelect("/key5"); // Get first result by BFS order, then the result here would be "true"
```
