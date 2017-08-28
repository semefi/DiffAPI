# DiffAPI

A WebAPI to get differences between base64 encoded data.

## Getting Started

The project is a WebAPI developed in C#

### Prerequisites

Restore the Nuget Packages

```
-Swagger
-Dapper
-Newtonsoft.Json
-Moq
-nUnit
-Autofixture
```

### Installing

Follow the next steps:

```
1. Clone the repository
2. Restore the nuget packages
3. Use the DiffDBs.sql inside the DBScripts folder, to create the DB and the UnitTest DB 
4. Enjoy!
```

Now, you should be able to run the project. The service's endpoint are the next ones:

```
[POST] http:\\[host]:[port]\v1\diff\[id]\left
[POST] http:\\[host]:[port]\v1\diff\[id]\right
[GET]  http:\\[host]:[port]\v1\diff\[id]
```

To save or update the left side of the comparison use the first one.
To save or update the right side of the comparison use the second one.

```
NOTE: You MUST use the same Id for both sides and call both endpoints before trying to get the differences.
```

To get the differences between both sides you have to call the third one.

Example:

```
First, we assign both sides , using the first endpoints and sending the info to be Diff-ed on the Body

[POST] http:\\[host]:[port]\v1\diff\12345\left [Parameter in BODY] [TestStringInBase64]
[POST] http:\\[host]:[port]\v1\diff\12345\right [Parameter in BODY] [TESTStringInBase64]

Last, we call the last endpoint to get the differences

[GET] http:\\[host]:[port]\v1\diff\12345

Now, you will have a response to know if the strings are same size or not, are equal or not.
If are same size but not equal, you will see a detailed information about the differences with offset and length of each one of them.
```


## Built With

* [Dapper](https://github.com/StackExchange/Dapper) - The Micro-ORM
* [Newtonsoft](https://www.newtonsoft.com/json) - Json Framework for .Net
* [Swagger](https://swagger.io/) - Framework of API Developer Tools

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Sebastian Mendez** - *Initial work* - [semefi](https://github.com/semefi)

See also the list of [contributors](https://github.com/semefi/DiffAPI/contributors) who participated in this project.


