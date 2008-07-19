@echo off
REM *******************************************************************
REM ***                                                             ***
REM ***                   "Copy Build"                              ***
REM ***                                                             ***
REM ***   	       Brian Preston: created...                        ***
REM ***   	          5:24 AM Wednsday, July 16, 2008               ***
REM ***                                                             ***
REM *******************************************************************

SETLOCAL

set CONFIG=%1
set SLN_NAME=%2

echo 	*	Update common library, began %date% %time%.
echo 	*	Copy output %CONFIG% to common lib directory.
echo 	*	Current Directory is "%CD%".

REM ******************** Body *************************************

rem todo: use windows "rd" command instead bash "rm" 
rem makes dir if not exsits
mkdir ..\..\..\BUILD\%CONFIG% 
rem clear directory
pushd ..\..\..\BUILD\%CONFIG% 
rm *.*
popd
rem copy files
xcopy *.* ..\..\..\BUILD\%CONFIG% /e /f /r
rem remove unwanted files (logs, debug, etc ...) 
pushd ..\..\..\BUILD\%CONFIG% 
rm *.pdb
rm *.log
rm *.vshost.exe.config

"C:\Program Files\WinZip\wzzip.exe" -a  ..\..\..\BUILD\%CONFIG%\%SLN_NAME%.zip

popd

REM ******************* Body ***************************************

if not ERRORLEVEL=0 goto Error

echo	*	Current Directory is "%CD%".
echo	*	Finished, Bye.

echo *	Update common library, ended %date% %time%.
goto End

REM Display error and end 
:Error
echo * An error occured while attemping to update.
echo * [ Error: %ERRORLEVEL% ]

:End
REM *******************************************************************
REM ***                    CHANGE LOG                               ***
REM ***     Date:	7-16-2008	Description:		created         ***
REM *******************************************************************
ENDLOCAL
@echo on
