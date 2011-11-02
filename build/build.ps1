properties {
	$base_dir = resolve-path .
    $sln_file_path = "$base_dir\..\src\GiveCRM.sln"
	$web_proj_folder = "$base_dir\..\src\GiveCRM.Web\"
	$configuration = "release"
    $package_dir = "$base_dir\..\package\"
}

#Loading external functions file
.\functions.ps1
Framework "4.0"

task default -depends Compile

task Clean {
    clean_directory $package_dir
}

task Compile -depends Clean {
    run_msbuild $sln_file_path
    move_package $web_proj_folder $package_dir $configuration
    clean_up_pdb_files $package_dir
}