param (
	$Configuration = ''
)

import-module .\psake\psake.psm1

invoke-psake ./build.ps1

remove-module psake
