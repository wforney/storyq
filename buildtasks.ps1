properties { 
	$rootDir  = Resolve-Path .
	$srcDir = "$rootDir\src"
	$buildOutputDir = "$rootDir\build" 
	$slnFile = "$rootDir\src\StoryQ.sln"
}

task default -depends CopyBuildOutput

task Clean {
	Remove-Item -force -recurse $buildOutputDir -ErrorAction SilentlyContinue 
	exec { msbuild $slnFile /t:Clean }
}

task Compile -depends Clean {
	exec { msbuild $slnFile /p:Configuration=Release }
}

task CopyBuildOutput -depends Compile {
	robocopy $rootDir\src\StoryQ\bin\Release $buildOutputDir
	robocopy $rootDir\src\StoryQ.Converter.Wpf\bin\Release $buildOutputDir StoryQ.Converter.Wpf.exe*
	robocopy $rootDir\src\StoryQ.Converter.Wpf\bin\Release\LanguagePacks $buildOutputDir\LanguagePacks
}