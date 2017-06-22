/******************************************************************************
**		Name: StoredProcedureLog
*******************************************************************************/

EXEC dbo.StoredProcedureLogDelete @StoredProcedureLogId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.StoredProcedureLogDelete @StoredProcedureLogId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.StoredProcedureLogDelete @StoredProcedureLogId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'

