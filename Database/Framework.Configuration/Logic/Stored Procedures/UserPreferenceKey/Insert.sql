IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyInsert')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyInsert'
	DROP  Procedure UserPreferenceKeyInsert
END
GO

PRINT 'Creating Procedure UserPreferenceKeyInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:UserPreferenceKeyInsert
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

CREATE Procedure dbo.UserPreferenceKeyInsert
(
		@UserPreferenceKeyId	INT			= NULL OUTPUT
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)		
	,	@Value					VARCHAR(50)		
	,	@DataTypeId				INT					
	,	@Description			VARCHAR(50)		
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50) = 'UserPreferenceKey'
)
AS
BEGIN

	SET NOCOUNT ON

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceKey', @UserPreferenceKeyId OUTPUT
		
	INSERT INTO dbo.UserPreferenceKey 
	( 
			UserPreferenceKeyId
		,	ApplicationId
		,	Name					
		,	Value			
		,	DataTypeId				
		,	Description				
		,	SortOrder				
	)
	VALUES 
	(  
			@UserPreferenceKeyId
		,	@ApplicationId	
		,	@Name					
		,	@Value			
		,	@DataTypeId				
		,	@Description			
		,	@SortOrder				
	)
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceKeyId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

 