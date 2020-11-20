# Source Structure

 src/ | **root source directory**<br>
 • /tests | **all test projects**<br>
 • /tests/{persistence layer} | **infrastructure specific test projects**<br>
 • /common/ | **core interfaces and types required by all implementations** <br>
 • /{persistence layer}/ | **infrastructure specific code** <br>
 • /migration/ | **migration support library** <br>

## Building and Testing

Compiling the project should work without and sophisticated platform setup.

 * With up-to-date installed on Windows 10 >= build 2004, open the `Crudy.sln` file and **Rebuild Solution**.
 * With up-to-date Visual Studio and the latest .NET SDK installed on Windows or Linux, open the `src/` folder and run `dotnet build`
 * First class Jetbrains Rider and Mac support TBD

Testing will require a UNIX environment and a docker host to be available in order to virtualize required infrastructure for the different persistence platforms.
On windows, WSL2 and Docker for Desktop are recommended.
To run a test project:

 1. Make sure docker is installed locally, is running, and your current user has the necessary permissions to run docker commands
 1. Rebuild the solution if necessary
 1. If there is a `test-setup.sh` script, run it
 1. Run `dotnet test` in the project directory 
 1. If there is a `test-teardown.sh` script, run it

# Architecture

The library provides a number of interfaces in the `Crudy.Common` that are implemented by source-generators. 
