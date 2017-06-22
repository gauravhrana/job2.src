IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeXApplicationUserSearch')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeXApplicationUserSearch'
	DROP  Procedure  FieldConfigurationModeXApplicationUserSearch
END
GO

PRINT 'Creating Procedure FieldConfigurationModeXApplicationUserSearch'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeXApplicationUserSearch
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
			EXEC FieldConfigurationModeXApplicationUserSearch NULL	, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearch NULL	, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearch 1		, 'K'	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearch 1		, NULL	, NULL
			EXEC FieldConfigurationModeXApplicationUserSearch NULL	, NULL	, 'W'

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
Create procedure FieldConfigurationModeXApplicationUserSearch
(
		@FieldConfigurationModeXApplicationUserId		INT				= NULL	
	,	@FieldConfigurationModeId						INT				= NULL	
	,	@ApplicationUserId								INT				= NULL
	,	@FieldConfigurationModeAccessModeId				INT				= NULL
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
		,	b.Name						AS	'FieldConfigurationMode'			
		,	c.ApplicationUserName		AS	'ApplicationUser'
		,	d.Name						AS	'FieldConfigurationModeAccessMode'					
	FROM		dbo.FieldConfigurationModeXApplicationUser			a
	INNER JOIN	dbo.FieldConfigurationMode							b	ON	a.FieldConfigurationModeId				=	b.FieldConfigurationModeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	c	ON	a.ApplicationUserId						=	c.ApplicationUserId
	INNER JOIN	dbo.FieldConfigurationModeAccessMode				d	ON	a.FieldConfigurationModeAccessModeId	=	d.FieldConfigurationModeAccessModeId
	WHERE   a.FieldConfigurationModeXApplicationUserId	= ISNULL(@FieldConfigurationModeXApplicationUserId, a.FieldConfigurationModeXApplicationUserId )
	AND		a.FieldConfigurationModeId					= ISNULL(@FieldConfigurationModeId, a.FieldConfigurationModeId )
	AND		a.ApplicationUserId							= ISNULL(@ApplicationUserId, a.ApplicationUserId)
	AND		a.FieldConfigurationModeAccessModeId		= ISNULL(@FieldConfigurationModeAccessModeId, a.FieldConfigurationModeAccessModeId)
	AND		a.ApplicationId								= ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.FieldConfigurationModeXApplicationUserId	ASC

	IF @AddAuditInfo = 1 
		BEGIN
	
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @FieldConfigurationModeXApplicationUserId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END

GO

