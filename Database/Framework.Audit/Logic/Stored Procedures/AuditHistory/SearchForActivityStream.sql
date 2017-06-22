IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistorySearchForActivityStream')
BEGIN
	PRINT 'Dropping Procedure AuditHistorySearchForActivityStream'
	DROP  Procedure  AuditHistorySearchForActivityStream
END
GO

PRINT 'Creating Procedure AuditHistorySearchForActivityStream'
GO
/******************************************************************************
**		File: 
**		Name: AuditHistorySearchForActivityStream
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.AuditHistorySearchForActivityStream
(
		@FromSearchDate			DATETIME		= NULL	
	,	@ToSearchDate			DATETIME		= NULL	
	,	@PersonId				INT				= NULL	
	,	@DataViewMode			VARCHAR(50)		= NULL		
	,	@PageSize				INT				= 100
	,	@PageIndex				INT				= 1	
	,	@OrderBy				VARCHAR(50)		= 'CreatedDate'
	,	@OrderByDirection		VARCHAR(4)		= 'DESC'
	,	@ExpirationSpan			INT				= 10			
	,	@AuditId				INT					
	,	@ApplicationId			INT				= NULL	
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AuditHistory'
)
AS
BEGIN

	DECLARE @SortClause		AS VARCHAR(50)
	DECLARE	@WhereClause	AS VARCHAR(500)	

	-- Prepare @SortClause and @WhereClause

	SET	@SortClause = @OrderBy
	SET @WhereClause = ' '

	IF	@PersonId	IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' @PersonId = '+ CONVERT(VARCHAR(10), @PersonId)
	END

	IF	@FromSearchDate	IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' @FromSearchDate = '+ CONVERT(VARCHAR(50), @FromSearchDate)
	END

	IF	@ToSearchDate	IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' @ToSearchDate = ' + CONVERT(VARCHAR(50), @ToSearchDate)
	END

	IF	@DataViewMode	IS NOT NULL
	BEGIN
		SET @WhereClause = @WhereClause + ' @DataViewMode = ' + @DataViewMode
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
	
		IF	@DataViewMode = 'Real Data'
		BEGIN

			INSERT INTO QuickPaginationIndex
			SELECT	@QuickPaginationRunId
				,	ROW_NUMBER()	
						OVER 
							( 
								ORDER BY CreatedDate DESC
							)AS ROW_NUMBER
				,	a.AuditHistoryId
				FROM		dbo.AuditHistory	a
				WHERE		a.CreatedDate			BETWEEN	 COALESCE(@FromSearchDate, a.CreatedDate)		AND	 COALESCE(@ToSearchDate, a.CreatedDate)
				AND			a.CreatedByPersonId		=		 COALESCE(@PersonId, a.CreatedByPersonId)
				AND			a.EntityKey				> 0	

		END
		ELSE IF @DataViewMode = 'Both'
		BEGIN

				INSERT INTO QuickPaginationIndex
				SELECT	@QuickPaginationRunId
					,	ROW_NUMBER()	
							OVER 
								( 
									ORDER BY CreatedDate DESC
								)AS ROW_NUMBER
					,	a.AuditHistoryId
				FROM		dbo.AuditHistory	a
				WHERE		a.CreatedDate			BETWEEN	 COALESCE(@FromSearchDate, a.CreatedDate)		AND	 COALESCE(@ToSearchDate, a.CreatedDate)
				AND			a.CreatedByPersonId		=		 COALESCE(@PersonId, a.CreatedByPersonId)	

		END
		ELSE
		BEGIN
		
				SELECT	@QuickPaginationRunId
					,	ROW_NUMBER()	
							OVER 
								( 
									ORDER BY CreatedDate DESC
								)AS ROW_NUMBER
					,	a.AuditHistoryId
				FROM		dbo.AuditHistory	a
				WHERE		a.CreatedDate			BETWEEN	 COALESCE(@FromSearchDate, a.CreatedDate)		AND	 COALESCE(@ToSearchDate, a.CreatedDate)
				AND			a.CreatedByPersonId		=		 COALESCE(@PersonId, a.CreatedByPersonId)
				AND			a.EntityKey				< 0	

		END

	END

	DECLARE @TotalRecords	AS	INT	= NULL

	SELECT		@TotalRecords = COUNT(a.QuickPaginationRunId)
	FROM		dbo.QuickPaginationIndex	a
	INNER JOIN	dbo.AuditHistory			b
		ON		a.EntityKey	=	b.AuditHistoryId
	WHERE	a.QuickPaginationRunId = @QuickPaginationRunId

	SELECT	e.QuickPaginationRunId
		,	e.RowNumber
		,	a.AuditHistoryId		
		,	a.SystemEntityId		
		,	a.EntityKey			
		,	a.AuditActionId		
		,	a.CreatedDate
		,	a.CreatedDate					AS 'Date'
		,	a.CreatedByPersonId
		,	b.EntityName					AS 'SystemEntity'
		,	c.Name							AS 'AuditAction'
		,	d.FirstName + ' ' + d.LastName	AS 'Action By'	
		,	d.FirstName + ' ' + d.LastName	AS 'Person'
		,	@TotalRecords		AS	'TotalCount'
	FROM		dbo.AuditHistory									a
	INNER JOIN	Configuration.dbo.SystemEntityType					b		
		ON		a.SystemEntityId = b.SystemEntityTypeId
	INNER JOIN	dbo.AuditAction										c					
		ON		a.AuditActionId = c.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser  d		
		ON		a.CreatedByPersonId = d.ApplicationUserId
	INNER JOIN	dbo.QuickPaginationIndex	e
		ON		a.AuditHistoryId	=	e.EntityKey
	WHERE	e.QuickPaginationRunId = @QuickPaginationRunId
	AND		e.RowNumber	<=	@PageSize * @PageIndex
	AND		e.RowNumber	>	@PageSize * (@PageIndex - 1)	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO