IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'QuickPaginationRunInsert')
BEGIN
	PRINT 'Dropping Procedure QuickPaginationRunInsert'
	DROP  Procedure QuickPaginationRunInsert
END
GO

PRINT 'Creating Procedure QuickPaginationRunInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:QuickPaginationRunInsert
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				WhereClause:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.QuickPaginationRunInsert
(
		@QuickPaginationRunId		INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT										
	,	@ApplicationUserId			INT			
	,	@SystemEntityTypeId			INT	
	,	@SortClause					VARCHAR(50)						
	,	@WhereClause				VARCHAR(500)		
	,	@ExpirationTime				DECIMAL(15,0)	= NULL
	,	@ExpirationSpan				INT				= NULL
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'QuickPaginationRun'
)
AS
BEGIN

	IF	@QuickPaginationRunId IS NULL AND @ExpirationSpan	IS NOT NULL
	BEGIN	

		DECLARE @CurrentSpan	AS DECIMAL(15, 0)	=	NULL
		SET		@CurrentSpan = CAST( CONVERT(VARCHAR(8), GETDATE(), 112) + REPLACE(CONVERT(varchar(5), GETDATE(), 114), ':', '') AS DECIMAL)
		
		SELECT  @QuickPaginationRunId	=	a.QuickPaginationRunId
		FROM	dbo.QuickPaginationRun	a
		WHERE	a.SortClause				=	@SortClause
		AND		ISNULL(a.WhereClause, -1)	=	ISNULL(@WhereClause, ISNULL(a.WhereClause, -1))
		AND		a.ExpirationTime			>=  @CurrentSpan

	END

	IF	@QuickPaginationRunId	IS NULL
	BEGIN

		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @QuickPaginationRunId OUTPUT, @AuditId

		IF	@ExpirationTime	IS NULL AND @ExpirationSpan IS NOT NULL
		BEGIN
			SET @ExpirationTime = CAST( CONVERT(VARCHAR(8), DATEADD(n, @ExpirationSpan, GETDATE()), 112) + REPLACE(CONVERT(varchar(5), DATEADD(n, @ExpirationSpan, GETDATE()), 114), ':', '') AS DECIMAL)	
		END

		INSERT INTO dbo.QuickPaginationRun 
		( 
				QuickPaginationRunId
			,	ApplicationId					
			,	SortClause						
			,	WhereClause					
			,	ApplicationUserId
			,	SystemEntityTypeId
			,	ExpirationTime						
		)
		VALUES 
		(		
				@QuickPaginationRunId
			,	@ApplicationId	
			,	@SortClause				
			,	@WhereClause				
			,	@ApplicationUserId
			,	@SystemEntityTypeId
			,	@ExpirationTime									
		)

	END

	SELECT @QuickPaginationRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @QuickPaginationRunId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 