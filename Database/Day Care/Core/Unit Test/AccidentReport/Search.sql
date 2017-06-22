/******************************************************************************
**		Name: AccidentReport
*******************************************************************************/

EXEC dbo.AccidentReport_Search @Name = 'Serious'   
EXEC dbo.AccidentReport_Search @Name = 'Normal'	
EXEC dbo.AccidentReport_Search @Name = 'Out of danger'