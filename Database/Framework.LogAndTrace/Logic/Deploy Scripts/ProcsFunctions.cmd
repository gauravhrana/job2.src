@Echo Off
if "%1"=="" GOTO Usage
if "%2"=="" GOTO Usage

ECHO -------------------------------------------------
ECHO Installing Views
ECHO -------------------------------------------------
rem for %%f in ("..\Views\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b
rem if errorlevel 1 pause

ECHO -------------------------------------------------
ECHO Installing Functions
ECHO -------------------------------------------------
for %%f in ("..\Functions\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b
if errorlevel 1 pause

ECHO -------------------------------------------------
ECHO Installing Stored Procs
ECHO -------------------------------------------------
for %%f in ("..\Stored Procedures\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\RenumberRun\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\RenumberRunDetail\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\RenumberRunIDTrack\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\StoredProcedureLog\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\StoredProcedureLogDetail\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\StoredProcedureLogRaw\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4

if errorlevel 1 pause
GOTO exit

:Usage
ECHO .
ECHO usage: Procedures SERVER_NAME DB_NAME
ECHO .

:exit
pause