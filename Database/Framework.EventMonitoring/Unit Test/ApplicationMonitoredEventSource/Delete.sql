/******************************************************************************
**		Name: ApplicationMonitoredEventSource
*******************************************************************************/

EXEC dbo.ApplicationMonitoredEventSourceDelete @ApplicationMonitoredEventSourceId = -111	, @Audit = 400	, @AuditDate = '12/12/2011'
EXEC dbo.ApplicationMonitoredEventSourceDelete @ApplicationMonitoredEventSourceId = -121	, @Audit = 400   , @AuditDate = '12/14/2011'
EXEC dbo.ApplicationMonitoredEventSourceDelete @ApplicationMonitoredEventSourceId = -311	, @Audit = 400   , @AuditDate = '12/16/2011'

