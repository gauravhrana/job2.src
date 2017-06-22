IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND Name='Log4NetSearch1')
BEGIN
	PRINT 'Dropping Procedure Log4NetSearch1'
	DROP Procedure Log4NetSearch1
END
GO

PRINT 'Creating Procedure Log4NetSearch1'
GO

/******************************************************************************
**		File: 
**		Level: Log4NetSearch1
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
			EXEC Log4NetSearch1 NULL	, NULL	, NULL
			EXEC Log4NetSearch1 NULL	, 'K'	, NULL
			EXEC Log4NetSearch1 1		, 'K'	, NULL
			EXEC Log4NetSearch1 1		, NULL	, NULL
			EXEC Log4NetSearch1 NULL	, NULL	, 'W'

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
CREATE PROCEDURE dbo.Log4NetSearch1
(
		@Id						INT				= NULL 		
	,	@ApplicationId			INT				= NULL 	
	,	@LogUser				VARCHAR(255)	= NULL
	,	@Logger					VARCHAR(255)	= NULL	
	,	@PageSize				INT				= 100
	,	@PageIndex				INT				= 1	
	,	@OrderBy				VARCHAR(50)		= 'Id'
	,	@OrderByDirection		VARCHAR(4)		= 'ASC'
	,	@ExpirationSpan			INT				= 10
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'Log4Net'	 
)
WITH RECOMPILE
AS
BEGIN	
	DECLARE @SortClause		AS VARCHAR(50)
	DECLARE	@WhereClause	AS VARCHAR(500)	

	-- Prepare @SortClause and @WhereClause

	SET	@SortClause = @OrderBy
	SET @WhereClause = ' '

	IF	@Logger	IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' @Logger = '+ @Logger
	END

	IF	@LogUser	IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' @LogUser = '+ @LogUser
	END

	IF	@ApplicationId	IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' @ApplicationId = ' + CONVERT(VARCHAR(10), @ApplicationId)
	END

	-- Call QuickPaginationRunInsert Method to check whether records are already present in detail table or new QPRId will be returned.
	
	DECLARE @QuickPaginationRunId	AS	INT	= NULL
	DECLARE @SystemEntityTypeId		AS	INT

	Select @SystemEntityTypeId		= dbo.GetSystemEntityTypeId(@SystemEntityType)

	EXEC	CommonServices.dbo.QuickPaginationRunInsert
			@QuickPaginationRunId	=	@QuickPaginationRunId	OUTPUT
		,	@ApplicationId			=	@ApplicationId
		,	@ApplicationUserId		=	@AuditId
		,	@SystemEntityTypeId		=	@SystemEntityTypeId
		,	@SortClause				=	@SortClause	
		,	@WhereClause			=	@WhereClause
		,	@ExpirationTime			=	NULL
		,	@ExpirationSpan			=	@ExpirationSpan
		,	@AuditId				=	@AuditId

	--SELECT @QuickPaginationRunId
	DECLARE @CntQPR AS INT

	-- IF there's record exist for particular QuickPaginationRunId then insert records into detail table.
	
	SELECT	@CntQPR = COUNT(QuickPaginationRunId) 
	FROM	dbo.QuickPaginationIndex
	WHERE	QuickPaginationRunId	=	@QuickPaginationRunId

	IF @CntQPR = 0
	BEGIN
		
		INSERT INTO QuickPaginationIndex
		SELECT	@QuickPaginationRunId
			,	ROW_NUMBER()	
					OVER 
						(
							ORDER BY 							
								CASE 
									WHEN @OrderBy = 'Id'	AND @OrderByDirection != 'ASC'
										THEN a.Id 
									END DESC,
								CASE 
									WHEN @OrderBy = 'Id'	AND @OrderByDirection = 'ASC'
										THEN a.Id 
									END,
								CASE 
									WHEN @OrderBy = 'Date'	AND @OrderByDirection != 'ASC'
										THEN a.Date 
									END DESC,
								CASE 
									WHEN @OrderBy = 'Date'	AND @OrderByDirection = 'ASC'
										THEN a.Date 
									END,
								CASE 
									WHEN @OrderBy = 'Level'	AND @OrderByDirection != 'ASC'
										THEN a.Level 
									END DESC,
								CASE 
									WHEN @OrderBy = 'Level'	AND @OrderByDirection = 'ASC'
										THEN a.Level 
									END							
								
							) AS ROW_NUMBER
			,	a.Id
		FROM	dbo.Log4Net a
		WHERE	a.Logger		= ISNULL(@Logger, a.Logger)
		AND		a.LogUser		= ISNULL(@LogUser, a.LogUser) 
		AND		a.ApplicationId	= ISNULL(@ApplicationId, a.ApplicationId)
	END

	DECLARE @TotalRecords	AS	INT	= NULL

	SELECT		@TotalRecords = COUNT(a.QuickPaginationRunId)
	FROM		dbo.QuickPaginationIndex	a
	INNER JOIN	dbo.Log4Net					b
		ON		a.EntityKey	=	b.Id
	WHERE	a.QuickPaginationRunId = @QuickPaginationRunId

	-- apply the paging
	SELECT	a.QuickPaginationRunId
		,	a.RowNumber
		,	b.Id
		,	b.LogUser
		,	b.ApplicationID
		,	b.Date
		,	b.StackTrace
		,	b.Thread
		,	b.Level
		,	b.Logger
		,	b.Message
		,	b.Computer
		,	b.Exception
		,	@TotalRecords		AS	'TotalCount'
	FROM		dbo.QuickPaginationIndex	a
	INNER JOIN	dbo.Log4Net					b
		ON		a.EntityKey	=	b.Id
	WHERE	a.QuickPaginationRunId = @QuickPaginationRunId
	AND		a.RowNumber	<=	@PageSize * @PageIndex
	AND		a.RowNumber	>	@PageSize * (@PageIndex - 1)		

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @Id
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
	

