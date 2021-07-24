# Crudy
A general purpose relational persistence library based on Ooorm.Data.

## Motivation

In brief, the goal of this library is to provide a "thin abstraction" over any relational, columnated storage resource, such as a SQL database.

In more detail,
 * Thin Abstraction
   - Library features should map closely and intuitively with relational database concepts.
   - If you know how to use a relational database there should be very little to learn in order to use this library.
   - Crudy should be a library, *not* a framework, and impose little to no restrictions on how other aspects of your project are architected. 
 * Code-driven
   - Your .NET DB entities should be the source of truth about your database-schema and migrations.
   - Nothing should be "stringly-typed," and IDE features such as find-references and name refactorings should work intuitively and reliably. 
 * Performant Enough
   - IO performance with Crudy should be comparable to other popular database libraries in .NET
   - The library should be as "GC friendly" as it can without incurring additional abstraction costs
 * Modern
   - The library should leverage modern C# features as of .NET 5.0 and not attempt to be compatible with previous styles or abstractions.
