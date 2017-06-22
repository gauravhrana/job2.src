/******************************************************************************
**		Name: BatchFile
*******************************************************************************/

EXEC dbo.BatchFileUpdate @BatchFileId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.BatchFileUpdate @BatchFileId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.BatchFileUpdate @BatchFileId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

