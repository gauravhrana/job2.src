IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationRoleSearch')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationRoleSearch'
	DROP  Procedure  FieldConfigurationModeXApplicationRoleSearch
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationRoleSearch'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationRoleSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC FieldConfigurationModeXApplicationRoleSearch NULL	, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearch NULL	, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearch 1		, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearch 1		, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure FieldConfigurationModeXApplicationRoleSearch
(
		@FieldConfigurationModeXApplicationRoleId		INT				= NULL	
	,	@FieldConfigurationModeId						INT				= NULL	
	,	@ApplicationRoleId								INT				= NULL	
	,	@FieldConfigurationModeAccessModeId				INT				= NULL
	,	@ApplicationId									INT				= NULL
	,	@AuditId										INT						
	,	@AuditDate										DATETIME		= NULL
	,	@SystemEntityType								VARCHAR(50)		= 'FieldConfigurationModeXApplicationRole' 	
	,	@ApplicationMode								INT				= NULL		
	,	@AddAuditInfo									INT				= 1
	,	@AddTraceInfo									INT				= 0
	,	@ReturnAuditInfo								INT				= 0
)
WITH RECOMPILE
AS
BEGIN
	
	SET	NOCOUNT ON

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
	WHERE   a.FieldConfigurationModeXApplicationRoleId	= ISNULL(@FieldConfigurationModeXApplicationRoleId, a.FieldConfigurationModeXApplicationRoleId )
	AND		a.FieldConfigurationModeId					= ISNULL(@FieldConfigurationModeId, a.FieldConfigurationModeId )
	AND		a.ApplicationRoleId							= ISNULL(@ApplicationRoleId, a.ApplicationRoleId)
	AND		a.FieldConfigurationModeAccessModeId		= ISNULL(@FieldConfigurationModeAccessModeId, a.FieldConfigurationModeAccessModeId)
	AND		a.ApplicationId								= ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.FieldConfigurationModeXApplicationRoleId	ASC

	IF @AddAuditInfo = 1 
		BEGIN
	
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @FieldConfigurationModeXApplicationRoleId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END

GO

