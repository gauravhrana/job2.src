IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryInsert'
	DROP  Procedure UserPreferenceCategoryInsert
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryInsert'
GO

/*********************************************************************************************
**		Task: 
**		Name:UserPreferenceCategoryInsert
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

CREATE Procedure dbo.UserPreferenceCategoryInsert
(
		@UserPreferenceCategoryId		INT				= NULL 	OUTPUT	
	,	@ApplicationId					INT		
	,	@Name							VARCHAR(100)							
	,	@Description					VARCHAR(500)							
	,	@SortOrder						INT									
	,	@AuditId						INT										
	,	@AuditDate						DATETIME		= NULL	
	,	@SystemEntityType				VARCHAR(50)		= 'UserPreferenceCategory'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceCategory', @UserPreferenceCategoryId OUTPUT
		
	INSERT INTO dbo.UserPreferenceCategory 
	( 
			UserPreferenceCategoryId
		,	ApplicationId							
		,	Name					
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@UserPreferenceCategoryId
		,	@ApplicationId			
		,	@Name					
		,	@Description			
		,	@SortOrder			
	)

	SELECT @UserPreferenceCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 