/******************************************************************************
**		Name: FileType
*******************************************************************************/

EXEC dbo.FileTypeUpdate @FileTypeId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
EXEC dbo.FileTypeUpdate @FileTypeId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
EXEC dbo.FileTypeUpdate @FileTypeId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8

