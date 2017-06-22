@Echo Off
if "%1"=="" GOTO Usage
if "%2"=="" GOTO Usage

ECHO -------------------------------------------------
ECHO Installing Create Table Script
ECHO -------------------------------------------------
sqlcmd -S %1 -d %2 -i "CreateTable.sql" -b -U %3 -P %4

ECHO -------------------------------------------------
ECHO Installing PK Script
ECHO -------------------------------------------------
sqlcmd -S %1 -d %2 -i "PrimaryKey.sql" -b -U %3 -P %4

ECHO -------------------------------------------------
ECHO Installing Unique Script
ECHO -------------------------------------------------
sqlcmd -S %1 -d %2 -i "UniqueKey.sql" -b -U %3 -P %4

ECHO -------------------------------------------------
ECHO Installing Search Script
ECHO -------------------------------------------------
sqlcmd -S %1 -d %2 -i "SearchProcedure.sql" -b -U %3 -P %4

ECHO -------------------------------------------------
ECHO Installing Insert Script
ECHO -------------------------------------------------
sqlcmd -S %1 -d %2 -i "InsertProcedure.sql" -b -U %3 -P %4

ECHO -------------------------------------------------
ECHO Installing Update Script
ECHO -------------------------------------------------
sqlcmd -S %1 -d %2 -i "UpdateProcedure.sql" -b -U %3 -P %4

ECHO -------------------------------------------------
ECHO Installing Delete Script
ECHO -------------------------------------------------
sqlcmd -S %1 -d %2 -i "DeleteProcedure.sql" -b -U %3 -P %4

if errorlevel 1 pause
GOTO exit

:Usage
ECHO .
ECHO usage: Procedures SERVER_NAME DB_NAME
ECHO .

:exit
pause
