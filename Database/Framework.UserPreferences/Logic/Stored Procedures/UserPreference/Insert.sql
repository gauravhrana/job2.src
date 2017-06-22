IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceInsert')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceInsert'
	DROP  Procedure UserPreferenceInsert
END
GO

PRINT 'Creating Procedure UserPreferenceInsert'
GO

/*********************************************************************************************
**		File: 
**		PersonId:UserPreferenceInsert
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
**********************************************************************************************/

CREATE Procedure dbo.UserPreferenceInsert
(
		@UserPreferenceId			INT			= NULL 		OUTPUT	
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

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreference', @UserPreferenceId OUTPUT
		
	INSERT INTO dbo.UserPreference 
	( 
			UserPreferenceId							
		,	ApplicationUserId					
		,	UserPreferenceCategoryId	
		,	UserPreferenceKeyId			
		,	Value						
		,	DataTypeId					
		,	ApplicationId
	)
	VALUES 
	(  
			@UserPreferenceId			
		,	@ApplicationUserId					
		,	@UserPreferenceCategoryId	
		,	@UserPreferenceKeyId		
		,	@Value						
		,	@DataTypeId					
		,	@ApplicationId
	)

	SELECT @UserPreferenceId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
