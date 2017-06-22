IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND Name='DatabaseChangeLogSearch')
BEGIN
	PRINT 'Dropping Procedure DatabaseChangeLogSearch'
	DROP Procedure DatabaseChangeLogSearch
END
GO

PRINT 'Creating Procedure DatabaseChangeLogSearch'
GO

/******************************************************************************
**		File: 
**		Level: DatabaseChangeLogSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC DatabaseChangeLogSearch NULL	, NULL	, NULL
			EXEC DatabaseChangeLogSearch NULL	, 'K'	, NULL
			EXEC DatabaseChangeLogSearch 1		, 'K'	, NULL
			EXEC DatabaseChangeLogSearch 1		, NULL	, NULL
			EXEC DatabaseChangeLogSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure dbo.DatabaseChangeLogSearch
(
		@Id						INT				= NULL 		
	,	@CurrentUser			VARCHAR(100)	= NULL
	,	@DataBaseName			VARCHAR(255)	= NULL
	,	@ObjectName				VARCHAR(255)	= NULL
	,	@ObjectType				VARCHAR(255)	= NULL
	,	@RecordDate				DATETIME		= NULL
	,	@FromSearchDate			DATETIME		= NULL
	,	@ToSearchDate			DATETIME		= NULL
	,	@HostName				VARCHAR(100)	= NULL
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'DatabaseChangeLog'
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0	 
)
WITH RECOMPILE
AS
BEGIN

	SET @CurrentUser	= ISNULL(@CurrentUser, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@CurrentUser))) = 0
		BEGIN
			SET	@CurrentUser = '%'
		END

	SET @DataBaseName	= ISNULL(@DataBaseName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@DataBaseName))) = 0
		BEGIN
			SET	@DataBaseName = '%'
		END

	SET @ObjectName	= ISNULL(@ObjectName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@ObjectName))) = 0
		BEGIN
			SET	@ObjectName = '%'
		END

	SET @ObjectType	= ISNULL(@ObjectType, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@ObjectType))) = 0
		BEGIN
			SET	@ObjectType = '%'
		END

	SET @HostName	= ISNULL(@HostName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@HostName))) = 0
		BEGIN
			SET	@HostName = '%'
		END

	SELECT		Id			
			,	DatabaseName
			,	SchemaName	
			,	ObjectName	
			,	ObjectType	
			,	EventType	
			,	RecordDate	
			,	SystemUser	
			,	CurrentUser	
			,	OriginalUser
			,	CommandText	
			,	EventData	
			,	HostName	
			,	Id				AS 'DatabaseChangeLogId'	        
	FROM	dbo.DatabaseChangeLog  a
	WHERE	CurrentUser		LIKE @CurrentUser + '%'
	AND		DataBaseName	LIKE @DataBaseName + '%'
	AND		ObjectName		LIKE @ObjectName + '%'
	AND		ObjectType		LIKE @ObjectType + '%'
	AND		HostName		LIKE @HostName + '%'
	AND		Id				=	ISNULL(@Id, a.Id)
	AND		a.RecordDate >=
			CASE
				WHEN @FromSearchDate IS NULL THEN a.RecordDate
				ELSE @FromSearchDate
			END
	AND		a.RecordDate <=
			CASE
				WHEN @ToSearchDate IS NULL THEN a.RecordDate
				ELSE @ToSearchDate
			END
	ORDER BY a.RecordDate		DESC
		,	 Id			ASC

	IF @AddAuditInfo = 1 
		BEGIN

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert			
				@SystemEntityType		= @SystemEntityType
			,	@EntityKey				= @Id
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId
		
		END
END
GO
	

