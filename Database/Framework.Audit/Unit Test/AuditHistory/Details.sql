/******************************************************************************
**		Name: AuditHistory
*******************************************************************************/

EXEC dbo.AuditHistoryUpdate @AuditHistoryId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.AuditHistoryUpdate @AuditHistoryId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.AuditHistoryUpdate @AuditHistoryId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

