import-module .\psake\psake.psm1

properties {
	$Configuration = "release"
	$run_type = "build"
}

switch($run_type)
{
	"deploy" { Invoke-psake ./deloy.ps1 }
	default { Invoke-psake ./build.ps1 $Configuration }
}

remove-module psake
