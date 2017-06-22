/******************************************************************************
**		Name: ApplicationMonitoredEventProcessingState
*******************************************************************************/

EXEC dbo.ApplicationMonitoredEventProcessingStateUpdate @ApplicationMonitoredEventProcessingStateId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ApplicationMonitoredEventProcessingStateUpdate @ApplicationMonitoredEventProcessingStateId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ApplicationMonitoredEventProcessingStateUpdate @ApplicationMonitoredEventProcessingStateId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

