/******************************************************************************
**		Name: ReleaseLog
*******************************************************************************/


--EXEC dbo.ReleaseLogUpdate @ReleaseLogId = -11	, @Name = 'Cheque'    , @Description = 'NotReceived'	, @SortOrder = 5
--EXEC dbo.ReleaseLogUpdate @ReleaseLogId = -12	, @Name = 'Bill'      , @Description = 'Paid'           , @SortOrder = 6
--EXEC dbo.ReleaseLogUpdate @ReleaseLogId = -81	, @Name = 'Payment'   , @Description = 'Received'       , @SortOrder = 8


EXEC dbo.ReleaseLogInsert
		@ReleaseLogId			=		-5011
	,	@ApplicationId			=		100
	,	@Name					=		'16-Sep-2012'						
	,	@VersionNo				=		'1'							
	,	@ReleaseDate			=		'2012-09-16 06:59:46.233'					
	,	@Description			=		'Release notes for 2012-September-16'					
	,	@SortOrder				=		1
	,	@AuditId				=		10

EXEC dbo.ReleaseLogInsert
		@ReleaseLogId			=		-5012
	,	@ApplicationId			=		100
	,	@Name					=		'09-Sep-2012'						
	,	@VersionNo				=		'1'							
	,	@ReleaseDate			=		'2012-09-09 06:59:46.233'					
	,	@Description			=		'Release notes for 2012-September-09'					
	,	@SortOrder				=		1
	,	@AuditId				=		10

EXEC dbo.ReleaseLogInsert
		@ReleaseLogId			=		-5013
	,	@ApplicationId			=		100
	,	@Name					=		'02-Sep-2012'						
	,	@VersionNo				=		'1'							
	,	@ReleaseDate			=		'2012-09-02 06:59:46.233'					
	,	@Description			=		'Release notes for 2012-September-02'					
	,	@SortOrder				=		1
	,	@AuditId				=		10



