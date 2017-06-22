IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginStatusInsert')
BEGIN
	PRINT 'Dropping Procedure UserLoginStatusInsert'
	DROP  Procedure UserLoginStatusInsert
END
GO

PRINT 'Creating Procedure UserLoginStatusInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:UserLoginStatusInsert
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

CREATE Procedure dbo.UserLoginStatusInsert
(
		@UserLoginStatusId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'UserLoginStatus'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @UserLoginStatusId OUTPUT, @AuditId
	
	INSERT INTO dbo.UserLoginStatus 
	( 
			UserLoginStatusId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@UserLoginStatusId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserLoginStatusId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 