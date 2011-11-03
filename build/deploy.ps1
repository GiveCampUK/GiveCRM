properties {
	$release_dir = "c:\websites\test.givecrm.org.uk"
}

$package_dir = "$base_dir\..\package\"
$src_folder = "$base_dir\..\src"

task default -depends Deploy

task Clean {
    clean_directory $release_dir
}

task Deploy -depends Clean {
    move_package $package_dir $release_dir
}