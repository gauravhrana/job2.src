/******************************************************************************
**		Name: ReleaseLogDetails
*******************************************************************************/

EXEC dbo.ReleaseLogDetailsUpdate @ReleaseLogDetailsId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.ReleaseLogDetailsUpdate @ReleaseLogDetailsId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.ReleaseLogDetailsUpdate @ReleaseLogDetailsId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

