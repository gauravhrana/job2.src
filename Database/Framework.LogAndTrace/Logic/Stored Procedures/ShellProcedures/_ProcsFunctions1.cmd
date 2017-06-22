﻿
@Echo Off
if "%1"=="" GOTO Usage
if "%2"=="" GOTO Usage

ECHO -------------------------------------------------
ECHO Installing Stored Procs
ECHO ----------------------------------------------

sqlcmd -S %1 -d %2 -i ".\StoredProcedureLogInsert.sql" -b -U %3 -P %4

if errorlevel 1 pause
GOTO exit

:Usage
ECHO .
ECHO usage: Procedures SERVER_NAME DB_NAME
ECHO .

:exit
pause
