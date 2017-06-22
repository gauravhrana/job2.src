IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditActionChildrenGet')
BEGIN
	PRINT 'Dropping Procedure AuditActionChildrenGet'
	DROP  Procedure AuditActionChildrenGet
END
GO

PRINT 'Creating Procedure AuditActionChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: AuditActionChildrenGet
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
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.AuditActionChildrenGet
(
		@AuditActionId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'AuditAction'
)
AS
BEGIN

	-- GET AuditHistory Records
	SELECT		a.AuditHistoryId		
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
	FROM		dbo.AuditHistory									a
	INNER JOIN	Configuration.dbo.SystemEntityType					b		ON		a.SystemEntityId = b.SystemEntityTypeId
	INNER JOIN	dbo.AuditAction										c		ON		a.AuditActionId = c.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser  d		ON		a.CreatedByPersonId = d.ApplicationUserId
	WHERE	a.AuditActionId = @AuditActionId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AuditActionId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   