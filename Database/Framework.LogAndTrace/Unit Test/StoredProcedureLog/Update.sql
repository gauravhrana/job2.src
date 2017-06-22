/******************************************************************************
**		Name: StoredProcedureLog
*******************************************************************************/

EXEC dbo.StoredProcedureLogUpdate @StoredProcedureLogId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.StoredProcedureLogUpdate @StoredProcedureLogId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.StoredProcedureLogUpdate @StoredProcedureLogId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

