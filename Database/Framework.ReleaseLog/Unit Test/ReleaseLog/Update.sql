/******************************************************************************
**		Name: ReleaseLog
*******************************************************************************/

EXEC dbo.ReleaseLogUpdate @ReleaseLogId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ReleaseLogUpdate @ReleaseLogId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ReleaseLogUpdate @ReleaseLogId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

