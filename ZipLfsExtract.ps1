# expand LFS based files from a zip

# copy the zip files path from ZipLfsCreate.ps1. It's the line ending with .zip"
$zipPathFromCreate = get-content .\ZipLfsCreate.ps1 | Select-String -Pattern '.zip"$' | Out-String

# the first '\\' is a regex pattern and the second isn't.
$zipFile = ($zipPathFromCreate -replace '\\', '\\' -replace '"', '' | ConvertFrom-StringData).Values[0]

# destination of extracted files is the same as where this *.ps1 script is executed from
$extractPath = $PSScriptRoot

# extract the contents of the zip
Expand-Archive -Path $zipFile -DestinationPath $extractPath -Force
