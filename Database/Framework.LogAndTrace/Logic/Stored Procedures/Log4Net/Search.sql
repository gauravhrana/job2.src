IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND Name='Log4NetSearch')
BEGIN
	PRINT 'Dropping Procedure Log4NetSearch'
	DROP Procedure Log4NetSearch
END
GO

PRINT 'Creating Procedure Log4NetSearch'
GO

/******************************************************************************
**		File: 
**		Level: Log4NetSearch
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
			EXEC Log4NetSearch NULL	, NULL	, NULL
			EXEC Log4NetSearch NULL	, 'K'	, NULL
			EXEC Log4NetSearch 1		, 'K'	, NULL
			EXEC Log4NetSearch 1		, NULL	, NULL
			EXEC Log4NetSearch NULL	, NULL	, 'W'

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
CREATE procedure dbo.Log4NetSearch
(
		@Id						INT				= NULL 		
	,	@ApplicationId			INT				= NULL 	
	,	@LogUser				VARCHAR(255)	= NULL
	,	@Logger					VARCHAR(255)	= NULL
	,	@Exception				VARCHAR(255)	= NULL
	,	@Message				VARCHAR(255)	= NULL
	,	@Level					VARCHAR(100)	= NULL
	,	@Date					DATETIME		= NULL
	,	@ConnectionKey			VARCHAR(100)	= NULL
	,	@Computer				VARCHAR(100)	= NULL
	,	@NoOfRecords			INT				= 100
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'Log4Net'		
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0 
)
WITH RECOMPILE
AS
BEGIN
	
	SET NOCOUNT ON
	SET	ROWCOUNT @NoOfRecords
	
	IF (@Message IS null)
	BEGIN
		SET	@Message = '%'
	END 
	
	IF (@Exception is NULL) 
	BEGIN
		SET	@Exception = '%'
	END
	 	
	SELECT	a.Id
		,	a.LogUser
		,	a.ApplicationID
		,	a.Date
		,	a.StackTrace
		,	a.Thread
		,	a.Level
		,	a.Logger
		,	a.Message
		,	a.Computer
		,	a.Exception
		,	a.ConnectionKey
	FROM	dbo.Log4Net a
	WHERE	a.Logger		= ISNULL(@Logger, a.Logger)
	AND		a.LogUser		= ISNULL(@LogUser, a.LogUser)	
	AND		a.ConnectionKey	= ISNULL(@ConnectionKey, a.ConnectionKey)
	AND		a.Computer		= ISNULL(@Computer, a.Computer)
	AND		a.Level			= ISNULL(@Level, a.Level)
	--AND		a.Date			= ISNULL(@Date, a.Date)
	AND		a.Id			= ISNULL(@Id, a.Id)
	AND	CONVERT(char(10), a.Date,101)	= CONVERT(char(10), @Date,101)
	AND		a.Message		LIKE @Message + '%' 
   	AND		a.Exception		LIKE @Exception + '%'
	ORDER BY a.Date		DESC
		,	 Id			ASC

	SET ROWCOUNT 0

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

