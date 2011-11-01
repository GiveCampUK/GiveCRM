param (
	$Configuration = ''
)

cd /psake
import-module .\psake.psm1
cd ..

$psake.use_exit_on_error = $true

invoke-psake ./build.ps1

cd /psake
remove-module psake
cd ..