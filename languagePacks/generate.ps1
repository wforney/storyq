if(! (Test-Path bin)) {md bin}
if(! (Test-Path obj)) {md obj}

gc ..\src\StoryQ\StoryQ.flit.tt | % { $_ -replace '\$\(SolutionDir\)', "$(pwd)\" } > obj\StoryQ.flit.tt
$tt = $env:CommonProgramFiles+'\Microsoft Shared\TextTemplating\10.0\TextTransform.exe'
$csc = $env:windir+'\Microsoft.NET\Framework\v3.5\Csc.exe'

foreach ($file in dir src)
{
    Write-Host $file
    $fileNoExt = $file.BaseName
    echo "- Generating obj\StoryQ.flit.$fileNoExt code from $fileNoExt"    

	& $tt -P ..\lib\development -a !!nameSpace!StoryQ.$fileNoExt -a !!inputFile!src\$file -out obj\StoryQ.flit.$fileNoExt.g.cs obj\StoryQ.flit.tt 

	echo "- Generating PNG and SVG from$file - if dot is installed - www.graphviz.org"

	& 'dot' -Nfontsize=10 -Efontsize=10 -Tpng "-oobj\StoryQ.$fileNoExt.png" src\$file
	& 'dot' -Nfontsize=10 -Efontsize=10 -Tsvg "-oobj\StoryQ.$fileNoExt.svg" src\$file

    #todo: use psake to compile this
	echo - Compiling bin\StoryQ.$fileNoExt.dll from obj\StoryQ.flit.$fileNoExt.g.cs
	& $csc /nologo /noconfig /doc:bin\StoryQ.$fileNoExt.XML /reference:"$env:ProgramFiles\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll" /reference:$env:windir\Microsoft.NET\Framework\v2.0.50727\System.dll /reference:..\src\StoryQ\bin\Release\StoryQ.dll /out:bin\StoryQ.$fileNoExt.dll /target:library obj\StoryQ.flit.$fileNoExt.g.cs ..\src\StoryQ\Properties\AssemblyInfo.cs
}
