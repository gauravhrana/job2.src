/******************************************************************************
**		Name: AuditAction
*******************************************************************************/

EXEC dbo.AuditActionUpdate @AuditActionId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.AuditActionUpdate @AuditActionId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.AuditActionUpdate @AuditActionId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

