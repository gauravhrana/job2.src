/******************************************************************************
**		Name: BatchFileStatus
*******************************************************************************/

EXEC dbo.BatchFileStatusUpdate @BatchFileStatusId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.BatchFileStatusUpdate @BatchFileStatusId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.BatchFileStatusUpdate @BatchFileStatusId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

