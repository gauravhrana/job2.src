IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceClone')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceClone'
	DROP  Procedure UserPreferenceClone
END
GO

PRINT 'Creating Procedure UserPreferenceClone'
GO

/*********************************************************************************************
**		File: 
**		Name: UserPreferenceClone
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.UserPreferenceClone
(
		@UserPreferenceId			INT			= NULL 	OUTPUT		
	,	@ApplicationUserId			INT								
	,	@UserPreferenceCategoryId	INT								
	,	@UserPreferenceKeyId		INT								
	,	@Value						VARCHAR(50)						
	,	@DataTypeId					INT								
	,	@ApplicationId				INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL				
	,	@SystemEntityType			VARCHAR(50) = 'UserPreference'
)
AS
BEGIN

	IF @UserPreferenceId IS NULL OR @UserPreferenceId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreference', @UserPreferenceId OUTPUT
	END			
	
	SELECT	@ApplicationUserId			=	ApplicationUserId						
		,	@UserPreferenceCategoryId	=	UserPreferenceCategoryId			
		,	@UserPreferenceKeyId		=	UserPreferenceKeyId			
		,	@Value						=	Value						
		,	@DataTypeId					=	DataTypeId					
		,	@ApplicationId				=	ApplicationId				
	FROM	dbo.UserPreference 
	WHERE	UserPreferenceId			=	@UserPreferenceId

	EXEC dbo.UserPreferenceInsert 
			@UserPreferenceId			=	NULL
		,	@ApplicationUserId			=	@ApplicationUserId	
		,	@UserPreferenceCategoryId	=	@UserPreferenceCategoryId					
		,	@UserPreferenceKeyId		=	@UserPreferenceKeyId		
		,	@Value						=	@Value					
		,	@DataTypeId					=	@DataTypeId
		,	@ApplicationId				=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@UserPreferenceId
		,	@AuditAction			=	'Clone'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId	

END	
GO
