include "build_support.boo"

solution = "MarkEmbling.PostcodesIO.sln"
configuration = "Release"

target default, (clean, restore, compile, test):
  pass

desc "Cleans the solution"
target clean:
  exec("dotnet clean --configuration ${configuration}")

desc "Restore packages"
target restore:
  exec("dotnet restore")

desc "Compiles the solution"
target compile, (clean, restore):
  exec("dotnet build --configuration ${configuration}")

desc "Executes the tests"
target test, (compile):
  exec("dotnet test tests/MarkEmbling.PostcodesIO.Tests/MarkEmbling.PostcodesIO.Tests.csproj --configuration ${configuration} --no-build")

desc "Publishes the NuGet packages"
target publish, (compile, test):
  with FileList("src/MarkEmbling.PostcodesIO/bin/${configuration}"):
    .Include("*.nupkg")
    .ForEach def(file):
      filename = Path.GetFileName(file.FullName)
      if prompt("Publish ${filename}...?"):
        exec("dotnet nuget push \"${file.FullName}\" --source https://www.nuget.org")
