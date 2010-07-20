::TODO make this file portable (x86, c: etc)

@if not exist obj mkdir obj
@if not exist bin mkdir bin

for %%I in (src\*.txt) DO (
	:: generate the cs file
	"C:\Program Files (x86)\Common Files\Microsoft Shared\TextTemplating\1.2\TextTransform.exe" -P ..\lib\development -a !!nameSpace!StoryQ.%%~nI -a !!inputFile!src\%%~nI.txt -out obj\StoryQ.flit.%%~nI.g.cs ..\src\StoryQ\StoryQ.flit.tt 

	:: find Dot & generate some images?
	where /Q dot
	if %ERRORLEVEL% EQU 0 dot -Nfontsize=10 -Efontsize=10 -Tpng -oobj\StoryQ.%%~nI.txt.png src\%%~nI.txt
	if %ERRORLEVEL% EQU 0 dot -Nfontsize=10 -Efontsize=10 -Tsvg -oobj\StoryQ.%%~nI.txt.svg src\%%~nI.txt

	::compile the cs file
	C:\Windows\Microsoft.NET\Framework\v3.5\Csc.exe /noconfig /doc:bin\StoryQ.%%~nI.XML /reference:"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll" /reference:C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll /reference:..\src\StoryQ\bin\Release\StoryQ.dll /out:bin\StoryQ.%%~nI.dll /target:library obj\StoryQ.flit.%%~nI.g.cs ..\src\StoryQ\Properties\AssemblyInfo.cs

)