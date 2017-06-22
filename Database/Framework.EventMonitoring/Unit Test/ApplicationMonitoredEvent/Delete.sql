/******************************************************************************
**		Name: ApplicationMonitoredEvent
*******************************************************************************/

EXEC dbo.ApplicationMonitoredEventDelete @ApplicationMonitoredEventId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.ApplicationMonitoredEventDelete @ApplicationMonitoredEventId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.ApplicationMonitoredEventDelete @ApplicationMonitoredEventId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'

