
####THIS RUNS THE BUILD
import-module .\psake\psake.psm1
invoke-psake ./build.ps1 -properties @{configuration = "release"} 
remove-module psake


####THIS RUNS THE DEPLOYMENT
import-module .\psake\psake.psm1
invoke-psake ./deploy.ps1
remove-module psake
