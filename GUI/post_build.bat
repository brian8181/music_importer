REM copy all SQL files
mkdir "$(TargetDir)sql"
xcopy "$(ProjectDir)sql" "$(TargetDir)sql" /S /Y
REM
REM copy all resouce files
mkdir "$(TargetDir)Resources"
xcopy "$(ProjectDir)Resources" "$(TargetDir)Resources" /S /Y