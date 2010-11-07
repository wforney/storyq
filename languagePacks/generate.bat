::TODO make this file portable (x86, c: etc)
@echo off

if not exist obj mkdir obj
if not exist bin mkdir bin


for %%I in (src\*.txt) DO (

	echo - Generating obj\StoryQ.flit.%%~nI.g.cs code from %%I       
	"%ProgramFiles(x86)%\Common Files\Microsoft Shared\TextTemplating\10\TextTransform.exe" -P ..\lib\development -a !!nameSpace!StoryQ.%%~nI -a !!inputFile!%%I -out obj\StoryQ.flit.%%~nI.g.cs ..\src\StoryQ\StoryQ.flit.tt 

	echo - Generating PNG and SVG from %%I - if dot is installed - www.graphviz.org
	where /Q dot
	if %ERRORLEVEL% EQU 0 dot -Nfontsize=10 -Efontsize=10 -Tpng -oobj\StoryQ.%%~nI.png %%I
	if %ERRORLEVEL% EQU 0 dot -Nfontsize=10 -Efontsize=10 -Tsvg -oobj\StoryQ.%%~nI.svg %%I

	echo - Compiling bin\StoryQ.%%~nI.dll from obj\StoryQ.flit.%%~nI.g.cs
	C:\Windows\Microsoft.NET\Framework\v3.5\Csc.exe /nologo /noconfig /doc:bin\StoryQ.%%~nI.XML /reference:"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll" /reference:C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll /reference:..\src\StoryQ\bin\Release\StoryQ.dll /out:bin\StoryQ.%%~nI.dll /target:library obj\StoryQ.flit.%%~nI.g.cs ..\src\StoryQ\Properties\AssemblyInfo.cs

)