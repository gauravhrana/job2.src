/******************************************************************************
**		Name: ApplicationMonitoredEvent
*******************************************************************************/

EXEC dbo.ApplicationMonitoredEventUpdate @ApplicationMonitoredEventId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationMonitoredEventUpdate @ApplicationMonitoredEventId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationMonitoredEventUpdate @ApplicationMonitoredEventId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

