IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationRoleSearchByFCModeAccessModeByFCModeAccessMode')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationRoleSearchByFCModeAccessModeByFCModeAccessMode'
	DROP  Procedure  FieldConfigurationModeXApplicationRoleSearchByFCModeAccessModeByFCModeAccessMode
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationRoleSearchByFCModeAccessModeByFCModeAccessMode'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationRoleSearchByFCModeAccessMode
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
			EXEC FieldConfigurationModeXApplicationRoleSearchByFCModeAccessMode NULL	, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearchByFCModeAccessMode NULL	, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearchByFCModeAccessMode 1		, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearchByFCModeAccessMode 1		, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationRoleSearchByFCModeAccessMode NULL	, NULL	, 'W'

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
Create procedure FieldConfigurationModeXApplicationRoleSearchByFCModeAccessModeByFCModeAccessMode
(
		@ApplicationRoleId								INT					
	,	@FieldConfigurationModeAccessMode				VARCHAR(50)		
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
	WHERE   a.ApplicationRoleId							= @ApplicationRoleId
	AND		d.Name										= @FieldConfigurationModeAccessMode	
	AND		a.ApplicationId								= ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.FieldConfigurationModeXApplicationRoleId	ASC

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= NULL
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END

GO

