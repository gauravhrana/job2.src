IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeveloperRoleDetails')
BEGIN
  PRINT 'Dropping Procedure DeveloperRoleDetails'
  DROP  Procedure DeveloperRoleDetails
END

GO

PRINT 'Creating Procedure DeveloperRoleDetails'
GO


/******************************************************************************
**		File: 
**		Name: DeveloperRoleDetails
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
CREATE Procedure dbo.DeveloperRoleDetails
(
			@DeveloperRoleId				INT					
		,	@AuditId				INT					
		,	@AuditDate				DATETIME	= NULL
		,	@SystemEntityType		VARCHAR(50) = 'DeveloperRole'
)
AS
BEGIN	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)	

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@DeveloperRoleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.DeveloperRoleId						
		,	a.Name							
		,	a.Description					
		,	a.SortOrder				
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
		,	b.Name					AS 'Application'	
		,	a.ApplicationId							
	FROM	dbo.DeveloperRole	a
	INNER JOIN AuthenticationAndAuthorization.dbo.Application b ON a.ApplicationId=b.ApplicationId
	WHERE	a.DeveloperRoleId = @DeveloperRoleId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DeveloperRole'
		,	@EntityKey				= @DeveloperRoleId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
  