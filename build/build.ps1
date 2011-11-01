properties {
	$base_dir = resolve-path .
	$sln_file_path = "$base_dir\..\src\GiveCRM.sln"
    $package_dir = "$base_dir\..\package\"
	$configuration = "debug"
}
Framework "4.0"

task default -depends Compile

task Compile {
    run_msbuild $sln_file_path 
}

#------------------------------------------------------------------------------------------------------------------#
#----------------------------------------Global Functions----------------------------------------------------------#
#------------------------------------------------------------------------------------------------------------------#

function global:run_msbuild ($solutionPath)
{
    exec { & msbuild $solutionPath "/t:rebuild" }
}