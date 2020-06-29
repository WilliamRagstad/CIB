# Install CIB - Windows

function Test-Administrator  
{  
    [OutputType([bool])]
    param()
    process {
        [Security.Principal.WindowsPrincipal]$user = [Security.Principal.WindowsIdentity]::GetCurrent();
        return $user.IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator);
    }
}

if(-not (Test-Administrator))
{
    # TODO: define proper exit codes for the given errors 
    Write-Host "This script must be executed as Administrator." -f red;
    exit 1;
}

Write-Host "=== CIB CLI Installation ===" -f Yellow;
$cibpath = "C:\Program Files\CIB\";

if (!(Test-Path $cibpath)) {
    Write-Host "Creating directory $cibpath" -f Yellow;
    New-Item -ItemType directory -Path $cibpath;
}

Write-Host "Cloning files..." -f Yellow;
$files = get-childitem $PWD
for($i = 0; $i -lt $files.Length; $i++) {
    if ($files[$i].BaseName -ne "install") {
        $dest = $cibpath + $files[$i].Name;
        write-host "Copying $dest";
        if (Test-Path $dest) { Remove-Item $dest; }
        Copy-Item -Path $files[$i].FullName -Destination $dest;
    }
}

# Copy-Item -Path * -Destination $cibpath -Force -Recurse

Write-Host "Updating Environment Variable PATH" -f Yellow;
if ($Env:Path.IndexOf($cibpath) -eq -1) {
    [System.Environment]::SetEnvironmentVariable("PATH", $Env:Path + ";" + $cibpath, "User")
}

Write-host "Sucessfully installed CIB CLI!" -f green;