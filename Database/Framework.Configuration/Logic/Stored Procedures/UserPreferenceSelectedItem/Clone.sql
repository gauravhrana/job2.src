IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceSelectedItemClone')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemClone'
	DROP  Procedure UserPreferenceSelectedItemClone
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemClone'
GO

/*********************************************************************************************
**		File: 
**		Name: UserPreferenceSelectedItemClone
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

CREATE Procedure dbo.UserPreferenceSelectedItemClone
(
		@UserPreferenceSelectedItemId		INT			= NULL 		OUTPUT	
	,	@ApplicationId						INT							
	,	@ApplicationUserId					INT							
	,	@UserPreferenceKeyId				INT							
	,	@ParentKey							VARCHAR(50)					
	,	@Value								VARCHAR(50)					
	,	@SortOrder							INT							
	,	@AuditId							INT								
	,	@AuditDate							DATETIME	= NULL				
	,	@SystemEntityType					VARCHAR(50) = 'UserPreferenceSelectedItem'
)
AS
BEGIN

	IF @UserPreferenceSelectedItemId IS NULL OR @UserPreferenceSelectedItemId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceSelectedItem', @UserPreferenceSelectedItemId OUTPUT
	END			
	
	SELECT	@ApplicationId						=	ApplicationId			
		,	@ApplicationUserId					=	ApplicationUserId				
		,	@UserPreferenceKeyId				=	UserPreferenceKeyId	
		,	@ParentKey							=	ParentKey				
		,	@Value								=	Value					
		,	@SortOrder							=	SortOrder				
	FROM	dbo.UserPreferenceSelectedItem 
	WHERE	UserPreferenceSelectedItemId		=	@UserPreferenceSelectedItemId

	EXEC dbo.UserPreferenceSelectedItemInsert 
			@UserPreferenceSelectedItemId		=	NULL
		,	@ApplicationId						=	@ApplicationId			
		,	@ApplicationUserId					=	@ApplicationUserId				
		,	@UserPreferenceKeyId				=	@UserPreferenceKeyId	
		,	@ParentKey							=	@ParentKey				
		,	@Value								=	@Value					
		,	@SortOrder							=	@SortOrder				

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@UserPreferenceSelectedItemId
		,	@AuditAction			=	'Clone'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId	

END	
GO
