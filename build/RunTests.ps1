$root = ".."

$nunitDir = dir $root\src\packages\nunit*
if ($nunitDir -eq $null) { "Could not find NUnit"; exit }

&$nunitDir\tools\nunit-console.exe /nologo $root\src\GiveCRM.sln
