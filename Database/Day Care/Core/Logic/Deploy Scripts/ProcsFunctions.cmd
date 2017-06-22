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
for %%f in ("..\Stored Procedures\AccidentPlace\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\AccidentReport\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Activity\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\ActivitySubType\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Bathroom\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Comment\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\DiaperStatus\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Discount\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\EventType\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\FirtsName\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\FoodType\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\LastName\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Meal\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\MealDetails\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\MealType\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\NeedItem\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\PaymentMethod\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\SickReport\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Sleep\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Student\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Teacher\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Tuition\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\ApplicationRole\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\Person\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4
for %%f in ("..\Stored Procedures\PersonXApplicationRole\*.sql") DO sqlcmd -S %1 -d %2 -i "%%f" -b -U %3 -P %4

if errorlevel 1 pause

GOTO exit

:Usage
ECHO .
ECHO usage: Procedures SERVER_NAME DB_NAME
ECHO .

:exit
pause