IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EntityOwnerList')
BEGIN
	PRINT 'Dropping Procedure EntityOwnerList'
	DROP  Procedure  dbo.EntityOwnerList
END
GO

PRINT 'Creating Procedure EntityOwnerList'
GO

/******************************************************************************
**		File: 
**		Name: EntityOwnerList
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
**		Date:		Author:				Developer:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.EntityOwnerList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'EntityOwner'
)
AS
BEGIN

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
	 WHERE	a.ApplicationId		=	@ApplicationId
	ORDER BY a.EntityOwnerId			ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO