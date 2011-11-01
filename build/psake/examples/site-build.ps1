properties {
    $script_dir = resolve-path .
	$base_dir = "C:\src\trunk\Site"
	$sln_file_path = "$base_dir\site.sln"
    $testAssemblies = "$base_dir\toptable.Site.Tasks.UnitTests\bin\Debug\toptable.Site.Tasks.UnitTests.dll"
    $nunit_dir = "C:\Program Files (x86)\NUnit 2.5.5\bin\net-2.0"
}

task default -depends Test

task Compile {
    run_msbuild $sln_file_path 
}

task Test -depends Compile {
    run_nunit "$testAssemblies"
}

















function global:run_nunit ($test_assembly)
{
    exec { & $nunit_dir\nunit-console-x86.exe $test_dir$test_assembly /nologo /nodots /xml=$result_dir$test_assembly.xml /exclude=DataLoader}
}

function global:run_msbuild ($solutionPath)
{
    exec { msbuild $solutionPath /t:rebuild }
}