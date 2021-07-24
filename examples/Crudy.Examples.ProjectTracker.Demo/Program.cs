using System;
using Crudy.Examples.ProjectTracker.Entities;

Console.WriteLine("Hello World!");

var storage = new MssqlStorage("connection string");


var timestamp = new Timestamp(new(), new(), new());

var createUser = User.New();