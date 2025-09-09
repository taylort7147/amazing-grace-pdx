setlocal 

set "project_dir=%~dp0.."
set "source_dir=%project_dir%\src"
set "antlr4_jar=%project_dir%\..\ThirdParty\antlr4\antlr-4.9.2-complete.jar"
set "classpath=.;%antlr4_jar%;%classpath%"
set "file_list=%source_dir%\Grammar\*.g4"
set "output_dir=%project_dir%\src\Grammar\Generated"
set "flags=-Dlanguage=CSharp -package BibleReferenceParser.Grammar.Generated -listener -no-visitor"

echo Project directory: %project_dir%
echo Source directory: %source_dir%
echo Output directory: %output_dir%
echo Classpath: %classpath%
echo Files: %file_list%

java org.antlr.v4.Tool -o "%output_dir%" %flags% %file_list%

endlocal
