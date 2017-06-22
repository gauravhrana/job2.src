IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'QuickPaginationRunDetails')
BEGIN
  PRINT 'Dropping Procedure QuickPaginationRunDetails'
  DROP  Procedure QuickPaginationRunDetails
END

GO

PRINT 'Creating Procedure QuickPaginationRunDetails'
GO


/******************************************************************************
**		File: 
**		Name: QuickPaginationRunDetails
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				WhereClause:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.QuickPaginationRunDetails
(
		@QuickPaginationRunId		INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'QuickPaginationRun'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@QuickPaginationRunId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.QuickPaginationRunId			
		,	a.ApplicationId		
		,	a.ApplicationUserId	
		,	a.SystemEntityTypeId
		,	a.SortClause						
		,	a.WhereClause	
		,	a.ExpirationTime
		,	b.EntityName			AS	'SystemEntityType'
		,	c.ApplicationUserName	AS	'ApplicationUserName'	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.QuickPaginationRun a 
	INNER JOIN	Configuration.dbo.SystemEntityType b
		ON	a.SystemEntityTypeId = b.SystemEntityTypeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c
		ON	a.ApplicationUserId = c.ApplicationUserId
	WHERE	QuickPaginationRunId = @QuickPaginationRunId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'QuickPaginationRun'
		,	@EntityKey				= @QuickPaginationRunId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   