IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyClone')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyClone'
	DROP  Procedure UserPreferenceKeyClone
END
GO

PRINT 'Creating Procedure UserPreferenceKeyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: UserPreferenceKeyClone
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

CREATE Procedure dbo.UserPreferenceKeyClone
(
		@UserPreferenceKeyId	INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)						
	,	@Value					VARCHAR(50)						
	,	@DataTypeId				INT									
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserPreferenceKey'
)
AS
BEGIN

	IF @UserPreferenceKeyId IS NULL OR @UserPreferenceKeyId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceKey', @UserPreferenceKeyId OUTPUT
	END			
	
	SELECT		@ApplicationId  =	ApplicationId
			,	@Name			=	Name					
			,	@Value			=	Value			
			,	@DataTypeId		=	DataTypeId				
			,	@Description	=	Description				
			,	@SortOrder		=	SortOrder									
	FROM	dbo.UserPreferenceKey 
	WHERE	UserPreferenceKeyId = @UserPreferenceKeyId

	EXEC dbo.UserPreferenceKeyInsert 
			@UserPreferenceKeyId	=	NULL
		,	@ApplicationId			=	@ApplicationId
		,	@Name					=	@Name
		,	@Value					=	@Value
		,	@DataTypeId				=	@DataTypeId
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceKeyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
