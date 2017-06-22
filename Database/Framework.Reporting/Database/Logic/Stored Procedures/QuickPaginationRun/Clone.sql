IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'QuickPaginationRunClone')
BEGIN
	PRINT 'Dropping Procedure QuickPaginationRunClone'
	DROP  Procedure QuickPaginationRunClone
END
GO

PRINT 'Creating Procedure QuickPaginationRunClone'
GO

/*********************************************************************************************
**		File: 
**		Name: QuickPaginationRunClone
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				WhereClause:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.QuickPaginationRunClone
(
		@QuickPaginationRunId		INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT										
	,	@ApplicationUserId			INT			
	,	@SystemEntityTypeId			INT	
	,	@SortClause					VARCHAR(50)						
	,	@WhereClause				VARCHAR(500)		
	,	@ExpirationTime				DECIMAL(15,0)			
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL				
	,	@SystemEntityType			VARCHAR(50)	= 'QuickPaginationRun'
)
AS
BEGIN					
	
	SELECT	@ApplicationId			= ApplicationId
		,	@WhereClause			= WhereClause
		,	@ApplicationUserId		= ApplicationUserId	
		,	@SystemEntityTypeId		= SystemEntityTypeId
		,	@ExpirationTime			= ExpirationTime				
	FROM	dbo.QuickPaginationRun
	WHERE   QuickPaginationRunId				= @QuickPaginationRunId
	ORDER BY QuickPaginationRunId

	EXEC dbo.QuickPaginationRunInsert 
			@QuickPaginationRunId	=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@SortClause				=	@SortClause
		,	@WhereClause			=	@WhereClause
		,	@ApplicationUserId		=	@ApplicationUserId
		,	@SystemEntityTypeId		=	@SystemEntityTypeId
		,	@ExpirationTime			=	@ExpirationTime
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'QuickPaginationRun'
		,	@EntityKey				= @QuickPaginationRunId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
