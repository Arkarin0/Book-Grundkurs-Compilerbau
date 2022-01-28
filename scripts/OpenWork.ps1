Set-StrictMode -version 2.0
#$ErrorActionPreference="Stop"

try {
    #jump to the repo root level.
    Push-Location ..

    #start solution in vs
    try{
        start .\Book-Grundkurs-Compilerbau.sln
    }
    catch{Write-Error "Coudn't start visual studio."}

    #start Roslyn-solution in vs-code
    try{ code ..\dotNet\roslyn }
    catch{Write-Error "Coudn't start visual Code."}
    
   #open usefull webpages
   try{
        $urls = @(
        "https://dev.azure.com/Arkarin0/Book%20-%20Basic%20Compiler%20Building/_boards/board/t/Book%20-%20Basic%20Compiler%20Building%20Team/Stories",
        "goolge.de")

    & "C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" --new-window $urls
   }
   catch{Write-Error "Coudn't open usefull webpages."}
   finally{}
}
catch [exception] {
    Write-Host $_
    Write-Host $_.Exception
    exit 1
}
finally {
    Pop-Location
}