IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusChildrenGet')
BEGIN
	PRINT 'Dropping Procedure FeatureOwnerStatusChildrenGet'
	DROP  Procedure FeatureOwnerStatusChildrenGet
END
GO

PRINT 'Creating Procedure FeatureOwnerStatusChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: FeatureOwnerStatusChildrenGet
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

CREATE Procedure dbo.FeatureOwnerStatusChildrenGet
(
		@FeatureOwnerStatusId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'FeatureOwnerStatus'
)
AS
BEGIN
	
	-- GET ModuleOwner Records
	SELECT	a.ModuleOwnerId			
		,	a.ApplicationId
		,	a.ModuleId
		,	a.DeveloperRoleId						
		,	a.Developer			
		,	a.FeatureOwnerStatusId	
		,	b.Name					AS	'Module'
		,	c.Name					AS	'DeveloperRole'
		,	d.Name					AS	'FeatureOwnerStatus'
	FROM		dbo.ModuleOwner		a
	INNER JOIN	dbo.Module			b
		ON	a.ModuleId			=	b.ModuleId
	INNER JOIN	dbo.DeveloperRole	c
		ON	a.DeveloperRoleId	=	c.DeveloperRoleId
	INNER JOIN	dbo.FeatureOwnerStatus	d
		ON	a.FeatureOwnerStatusId	=	d.FeatureOwnerStatusId
	WHERE	a.FeatureOwnerStatusId = @FeatureOwnerStatusId

	-- GET FunctionalityOwner Records
	SELECT	a.FunctionalityOwnerId			
		,	a.ApplicationId
		,	a.FunctionalityId
		,	a.DeveloperRoleId						
		,	a.Developer			
		,	a.FeatureOwnerStatusId	
		,	b.Name					AS	'Functionality'
		,	c.Name					AS	'DeveloperRole'
		,	d.Name					AS	'FeatureOwnerStatus'
	FROM		dbo.FunctionalityOwner		a
	INNER JOIN	dbo.Functionality			b
		ON	a.FunctionalityId			=	b.FunctionalityId
	INNER JOIN	dbo.DeveloperRole	c
		ON	a.DeveloperRoleId	=	c.DeveloperRoleId
	INNER JOIN	dbo.FeatureOwnerStatus	d
		ON	a.FeatureOwnerStatusId	=	d.FeatureOwnerStatusId
	WHERE	a.FeatureOwnerStatusId = @FeatureOwnerStatusId

	-- GET EntityOwner Records
	SELECT	a.EntityOwnerId			
		,	a.ApplicationId
		,	a.EntityId
		,	a.DeveloperRoleId						
		,	a.Developer			
		,	a.FeatureOwnerStatusId	
		,	b.EntityName			AS	'Entity'
		,	c.Name					AS	'DeveloperRole'
		,	d.Name					AS	'FeatureOwnerStatus'
	FROM		dbo.EntityOwner		a
	INNER JOIN	Configuration.dbo.SystemEntityType			b
		ON	a.EntityId			=	b.SystemEntityTypeId
	INNER JOIN	dbo.DeveloperRole	c
		ON	a.DeveloperRoleId	=	c.DeveloperRoleId
	INNER JOIN	dbo.FeatureOwnerStatus	d
		ON	a.FeatureOwnerStatusId	=	d.FeatureOwnerStatusId
	WHERE	a.FeatureOwnerStatusId = @FeatureOwnerStatusId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FeatureOwnerStatusId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   