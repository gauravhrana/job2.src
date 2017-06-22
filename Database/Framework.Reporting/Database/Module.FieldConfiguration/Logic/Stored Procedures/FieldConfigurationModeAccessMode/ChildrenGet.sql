IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeAccessModeChildrenGet')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeAccessModeChildrenGet'
	DROP  Procedure FieldConfigurationModeAccessModeChildrenGet
END
GO

PRINT 'Creating Procedure FieldConfigurationModeAccessModeChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeAccessModeChildrenGet
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

CREATE Procedure dbo.FieldConfigurationModeAccessModeChildrenGet
(
		@FieldConfigurationModeAccessModeId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'FieldConfigurationModeAccessMode'
)
AS
BEGIN

	-- GET FieldConfigurationModeAccessModeXProject Records
	SELECT	a.FieldConfigurationModeXApplicationRoleId	
		,	a.ApplicationId	
		,	a.FieldConfigurationModeId						
		,	a.ApplicationRoleId								
		,	a.FieldConfigurationModeAccessModeId	
		,	b.Name								AS	'FieldConfigurationMode'			
		,	c.Name								AS	'ApplicationRole'
		,	d.Name								AS	'FieldConfigurationModeAccessMode'				
	FROM		dbo.FieldConfigurationModeXApplicationRole			a
	INNER JOIN	dbo.FieldConfigurationMode							b	ON	a.FieldConfigurationModeId				=	b.FieldConfigurationModeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationRole	c	ON	a.ApplicationRoleId						=	c.ApplicationRoleId
	INNER JOIN	dbo.FieldConfigurationModeAccessMode				d	ON	a.FieldConfigurationModeAccessModeId	=	d.FieldConfigurationModeAccessModeId
	WHERE	a.FieldConfigurationModeAccessModeId = @FieldConfigurationModeAccessModeId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationModeAccessModeId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   