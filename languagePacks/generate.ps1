if(! (Test-Path bin)) {md bin}
if(! (Test-Path obj)) {md obj}

gc ..\src\StoryQ\StoryQ.flit.tt | % { $_ -replace '\$\(SolutionDir\)', "$(pwd)\" } > obj\StoryQ.flit.tt

foreach ($file in dir src)
{
    Write-Host $file
    $fileNoExt = $file.BaseName
    echo "- Generating obj\StoryQ.flit.$fileNoExt code from $fileNoExt"    
	& 'C:\Program Files (x86)\Common Files\Microsoft Shared\TextTemplating\1.2\TextTransform.exe' -P ..\lib\development -a !!nameSpace!StoryQ.$fileNoExt -a !!inputFile!src\$file -out obj\StoryQ.flit.$fileNoExt.g.cs obj\StoryQ.flit.tt 

	echo "- Generating PNG and SVG from$file - if dot is installed - www.graphviz.org"

	& 'dot' -Nfontsize=10 -Efontsize=10 -Tpng "-oobj\StoryQ.$fileNoExt.png" src\$file
	& 'dot' -Nfontsize=10 -Efontsize=10 -Tsvg "-oobj\StoryQ.$fileNoExt.svg" src\$file

	#echo - Compiling bin\StoryQ.$fileNoExt.dll from obj\StoryQ.flit.$fileNoExt.g.cs
	& 'C:\Windows\Microsoft.NET\Framework\v3.5\Csc.exe' /nologo /noconfig /doc:bin\StoryQ.$fileNoExt.XML /reference:"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll" /reference:C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll /reference:..\src\StoryQ\bin\Release\StoryQ.dll /out:bin\StoryQ.$fileNoExt.dll /target:library obj\StoryQ.flit.$fileNoExt.g.cs ..\src\StoryQ\Properties\AssemblyInfo.cs
}
