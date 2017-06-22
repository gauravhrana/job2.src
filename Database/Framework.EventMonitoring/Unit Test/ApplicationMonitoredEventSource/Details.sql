/******************************************************************************
**		Name: ApplicationMonitoredEventSource
*******************************************************************************/

EXEC dbo.ApplicationMonitoredEventSourceUpdate @ApplicationMonitoredEventSourceId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationMonitoredEventSourceUpdate @ApplicationMonitoredEventSourceId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationMonitoredEventSourceUpdate @ApplicationMonitoredEventSourceId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

