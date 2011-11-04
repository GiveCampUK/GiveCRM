properties {
    $sln_file_name = "GiveCRM.sln"
	$configuration = "release"
}

#Loading external functions file
.\functions.ps1

#SetUp Local Variables
Framework "4.0"
$base_dir = resolve-path .
$package_dir = "$base_dir\..\package\"
$src_folder = "$base_dir\..\src"

task default -depends Compile

task Compile {
    run_msbuild "$src_folder\$sln_file_name" "$configuration"
}