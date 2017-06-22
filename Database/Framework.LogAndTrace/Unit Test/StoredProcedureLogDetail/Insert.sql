/******************************************************************************
**		Name: StoredProcedureLogDetail
*******************************************************************************/

EXEC dbo.StoredProcedureLogDetailUpdate @StoredProcedureLogDetailId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.StoredProcedureLogDetailUpdate @StoredProcedureLogDetailId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.StoredProcedureLogDetailUpdate @StoredProcedureLogDetailId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

