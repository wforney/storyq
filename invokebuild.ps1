$error.Clear()

Import-Module .\lib\development\psake.psm1
Invoke-Psake buildtasks.ps1 default -framework "4.0"
Remove-Module psake

if ($error -ne '') {
	write-host "ERROR: $error" -fore RED;
	exit $error.Count
}