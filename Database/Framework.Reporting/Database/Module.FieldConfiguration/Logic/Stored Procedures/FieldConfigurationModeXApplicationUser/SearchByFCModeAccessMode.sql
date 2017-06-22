IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationUserSearchByFCModeAccessModeByFCModeAccessMode')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationUserSearchByFCModeAccessModeByFCModeAccessMode'
	DROP  Procedure  FieldConfigurationModeXApplicationUserSearchByFCModeAccessModeByFCModeAccessMode
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationUserSearchByFCModeAccessModeByFCModeAccessMode'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationUserSearchByFCModeAccessMode
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
			EXEC FieldConfigurationModeXApplicationUserSearchByFCModeAccessMode NULL	, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearchByFCModeAccessMode NULL	, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearchByFCModeAccessMode 1		, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearchByFCModeAccessMode 1		, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearchByFCModeAccessMode NULL	, NULL	, 'W'

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
Create procedure FieldConfigurationModeXApplicationUserSearchByFCModeAccessModeByFCModeAccessMode
(
		@ApplicationUserId								INT					
	,	@FieldConfigurationModeAccessMode				VARCHAR(50)		
	,	@ApplicationId									INT				= NULL
	,	@AuditId										INT						
	,	@AuditDate										DATETIME		= NULL
	,	@SystemEntityType								VARCHAR(50)		= 'FieldConfigurationModeXApplicationUser' 	
	,	@ApplicationMode								INT				= NULL		
	,	@AddAuditInfo									INT				= 1
	,	@AddTraceInfo									INT				= 0
	,	@ReturnAuditInfo								INT				= 0
)
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	SELECT	a.FieldConfigurationModeXApplicationUserId	
		,	a.ApplicationId	
		,	a.FieldConfigurationModeId						
		,	a.ApplicationUserId								
		,	a.FieldConfigurationModeAccessModeId	
		,	b.Name								AS	'FieldConfigurationMode'			
		,	c.ApplicationUserName				AS	'ApplicationUser'
		,	d.Name								AS	'FieldConfigurationModeAccessMode'				
	FROM		dbo.FieldConfigurationModeXApplicationUser			a
	INNER JOIN	dbo.FieldConfigurationMode							b	ON	a.FieldConfigurationModeId				=	b.FieldConfigurationModeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c	ON	a.ApplicationUserId						=	c.ApplicationUserId
	INNER JOIN	dbo.FieldConfigurationModeAccessMode				d	ON	a.FieldConfigurationModeAccessModeId	=	d.FieldConfigurationModeAccessModeId
	WHERE   a.ApplicationUserId							= @ApplicationUserId
	AND		d.Name										= @FieldConfigurationModeAccessMode	
	AND		a.ApplicationId								= ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.FieldConfigurationModeXApplicationUserId	ASC

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

