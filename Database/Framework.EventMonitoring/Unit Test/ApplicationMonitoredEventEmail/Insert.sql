/******************************************************************************
**		Name: ApplicationMonitoredEventEmail
*******************************************************************************/

EXEC dbo.ApplicationMonitoredEventEmailUpdate @ApplicationMonitoredEventEmailId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationMonitoredEventEmailUpdate @ApplicationMonitoredEventEmailId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationMonitoredEventEmailUpdate @ApplicationMonitoredEventEmailId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

