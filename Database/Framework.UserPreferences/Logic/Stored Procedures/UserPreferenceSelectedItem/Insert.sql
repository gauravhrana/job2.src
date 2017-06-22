IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceSelectedItemInsert')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemInsert'
	DROP  Procedure UserPreferenceSelectedItemInsert
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemInsert'
GO

/*********************************************************************************************
**		File: 
**		PersonId:UserPreferenceSelectedItemInsert
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

CREATE Procedure dbo.UserPreferenceSelectedItemInsert
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

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceSelectedItem', @UserPreferenceSelectedItemId OUTPUT
		
	INSERT INTO dbo.UserPreferenceSelectedItem 
	( 
			ApplicationId			
		,	ApplicationUserId		
		,	UserPreferenceKeyId		
		,	ParentKey				
		,	Value					
		,	SortOrder				
	)
	VALUES 
	(  
			@ApplicationId			
		,	@ApplicationUserId		
		,	@UserPreferenceKeyId		
		,	@ParentKey				
		,	@Value					
		,	@SortOrder	
	)


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceSelectedItemId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
