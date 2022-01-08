
function check-CSFiles {

    $replacements = @(
        #using namespaces or Explicit namespace callings.        
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.CodeGen"; replace= "Arkarin0.CodeAnalysis.CodeGen" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.Collections"; replace= "Arkarin0.CodeAnalysis.Collections" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.Debugging"; replace= "Arkarin0.CodeAnalysis.Debugging" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.FlowAnalysis"; replace= "Arkarin0.CodeAnalysis.FlowAnalysis" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.Operations"; replace= "Arkarin0.CodeAnalysis.Operations" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.PooledObjects"; replace= "Arkarin0.CodeAnalysis.PooledObjects" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.Symbols"; replace= "Arkarin0.CodeAnalysis.Symbols" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.Syntax"; replace= "Arkarin0.CodeAnalysis.Syntax" },
        [pscustomobject]@{ find= "Microsoft.CodeAnalysis.Syntax.InternalSyntax"; replace= "Arkarin0.CodeAnalysis.Syntax.InternalSyntax" },
        [pscustomobject]@{ find= "Roslyn.Utilities"; replace= "Sonea.Utilities" },

        #namespace replacement
        [pscustomobject]@{ find= "namespace Microsoft.CodeAnalysis"; replace= "namespace Arkarin0.CodeAnalysis" },
        
        #class remaning
        [pscustomobject]@{ find= "RoslynDebug"; replace= "SoneaDebug" },
        [pscustomobject]@{ find= "RoslynString"; replace= "SoneaString" },

        #replace source code
        [pscustomobject]@{ 
            find= "internal readonly record struct NodeStateEntry<T>(T Item, EntryState State, int OutputIndex, IncrementalGeneratorRunStep? Step);"; 
            replace= 
            "internal readonly struct NodeStateEntry<T>
        {
            public T Item { get; }
    
            public EntryState State { get; }
    
            public int OutputIndex { get; }
    
            public IncrementalGeneratorRunStep? Step { get; }
    
            public NodeStateEntry (T Item, EntryState State, int OutputIndex, IncrementalGeneratorRunStep? Step)
            {
                this.Item = Item;
                this.State = State;
                this.OutputIndex = OutputIndex;
                this.Step = Step;
            }
        }" }

    #{ find= "SourceText.LargeObjectHeapLimitInChars", replace= "SourceTextExtensions.LargeObjectHeapLimitInChars" }
    )
    
    $configFiles = Get-ChildItem . *.cs -rec
    Repalce-Core $configFiles $replacements
}

function Repalce-Core ($files, $replacements) {
    
    foreach ($file in $files)
    {
        $filePatchShort = $file.PSPath

        $content = Get-Content $file.PSPath
        $newContent = $content
        foreach ($replaceItem in $replacements){
            $find = $replaceItem.find
            $replace = $replaceItem.replace

            #Write-Host "f:$($find) r:$($replace)"
            
            $newContent= $newContent -replace $find, $replace
        }

        #if($content -ne $newContent)
        {
            try {
                Set-Content "$($file.PSPath)" $newContent
                Write-Host "$($filePatchShort): replaced"
            }
            catch {
                Write-Error "$($filePatchShort): $($Error[0].Exception))"
            }
        }
    }    
    
}


try {
    
    check-CSFiles

    Write-Host "finished"
    exit 1
}
catch {
    exit 0
}