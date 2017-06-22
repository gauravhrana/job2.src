IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleOwnerDetails')
BEGIN
  PRINT 'Dropping Procedure ModuleOwnerDetails'
  DROP  Procedure ModuleOwnerDetails
END

GO

PRINT 'Creating Procedure ModuleOwnerDetails'
GO


/******************************************************************************
**		File: 
**		Name: ModuleOwnerDetails
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
**		Date:		Author:				Developer:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ModuleOwnerDetails
(
		@ModuleOwnerId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ModuleOwner'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ModuleOwnerId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.ModuleOwnerId			
		,	a.ApplicationId
		,	a.ModuleId
		,	a.DeveloperRoleId						
		,	a.Developer			
		,	a.FeatureOwnerStatusId	
		,	b.Name					AS	'Module'
		,	c.Name					AS	'DeveloperRole'
		,	d.Name					AS	'FeatureOwnerStatus'	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.ModuleOwner		a
	INNER JOIN	dbo.Module			b
		ON	a.ModuleId			=	b.ModuleId
	INNER JOIN	dbo.DeveloperRole	c
		ON	a.DeveloperRoleId	=	c.DeveloperRoleId
	INNER JOIN	dbo.FeatureOwnerStatus	d
		ON	a.FeatureOwnerStatusId	=	d.FeatureOwnerStatusId
	WHERE	ModuleOwnerId = @ModuleOwnerId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ModuleOwner'
		,	@EntityKey				= @ModuleOwnerId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   