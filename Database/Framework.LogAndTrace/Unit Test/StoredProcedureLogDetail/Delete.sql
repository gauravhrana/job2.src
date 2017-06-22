/******************************************************************************
**		Name: StoredProcedureLogDetail
*******************************************************************************/

EXEC dbo.StoredProcedureLogDetailDelete @StoredProcedureLogDetailId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.StoredProcedureLogDetailDelete @StoredProcedureLogDetailId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.StoredProcedureLogDetailDelete @StoredProcedureLogDetailId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'

