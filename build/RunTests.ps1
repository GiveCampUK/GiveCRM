$root = ".."
$configuration = "Debug"

$nunitDir = dir $root\src\packages\nunit*
if ($nunitDir -eq $null) { "Could not find NUnit"; exit }

# The easy way was to test the solution, but this doesn't work with app.config's.
# &$nunitDir\tools\nunit-console.exe /nologo /config:$configuration $root\src\GiveCRM.sln

# Because of the app.config files, we find all test assemblies and test them individually.
$testProjects = dir $root\src -recurse -include *.csproj | where { (type $_.FullName) -match "nunit" }
foreach ($proj in $testProjects)
{
$assembly = "$($proj.DirectoryName)\bin\$configuration\$($proj.BaseName).dll"
Write-Output "Testing $($proj.BaseName)"
&$nunitDir\tools\nunit-console.exe /nologo $assembly
Write-Output " "
}
