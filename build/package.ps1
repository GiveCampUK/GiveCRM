#Loading external functions file
.\functions.ps1

#SetUp Local Variables
Framework "4.0"
$base_dir = resolve-path .
$package_dir = "$base_dir\..\package\"
$src_folder = "$base_dir\..\src"
$web_package_location = "$src_folder\GiveCRM.Web\obj\release\Package\PackageTmp"

task default -depends Package

task Clean {
    clean_directory $package_dir
}

task Package -depends Clean {
    move_package $web_package_location $package_dir
    clean_up_pdb_files $package_dir
}