# print current folder


# Cleaning up the project
dotnet clean WpfAppUI.csproj -c Release

# Deleting all the remaining bin and obj folders
Get-ChildItem .\ -include bin,obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }

# Adding the reference to the LibCNotDependency project
$xml = [xml] (Get-Content "WpfAppUI.csproj")

$itemGroup = $xml.Project.ItemGroup[1]
$newRef = $xml.CreateElement("ProjectReference", $xml.Project.ItemType.NamespaceURI)
$newRef.SetAttribute("Include", "..\LibCNotDependency\LibCNotDependency.csproj")
$itemGroup.AppendChild($newRef) | Out-Null

# $xml | Format-List *
# Saving the changes to the current folder

$dir = (Get-Location).tostring()
$xml.Save("$dir\WpfAppUIIncludeLibC.csproj")

# Building the project
dotnet publish WpfAppUIIncludeLibC.csproj -c Release -r win-x64 -p:PublishReadyToRun=true /p:DebugType=None /p:DebugSymbols=false --no-self-contained /p:PublishReadyToRunShowWarnings=true

# Delete WpfAppUIIncludeLibC.csproj file
Remove-Item WpfAppUIIncludeLibC.csproj