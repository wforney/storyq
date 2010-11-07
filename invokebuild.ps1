Import-Module .\lib\development\psake.psm1
Invoke-Psake buildtasks.ps1 default -framework "4.0"
Remove-Module psake