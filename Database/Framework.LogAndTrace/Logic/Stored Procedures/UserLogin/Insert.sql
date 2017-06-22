IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginInsert')
BEGIN
	PRINT 'Dropping Procedure UserLoginInsert'
	DROP  Procedure UserLoginInsert
END
GO

PRINT 'Creating Procedure UserLoginInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:UserLoginInsert
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
**		Date:		Author:				RecordDate:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.UserLoginInsert
(
		@UserLoginId			INT				= NULL 	OUTPUT	
	,   @ApplicationId			INT				= NULL	
	,	@UserName				VARCHAR(50)	 					
	,	@UserLoginStatusId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'UserLogin'
)
AS	
BEGIN 
	
	INSERT INTO dbo.UserLogin 
	( 
			ApplicationId					
		,	UserName						
		,	RecordDate					
		,	UserLoginStatusId						
	)
	VALUES 
	(  
			@ApplicationId	
		,	@UserName						
		,	GETDATE()				
		,	@UserLoginStatusId			
	)

	SELECT @UserLoginId = SCOPE_IDENTITY()

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserLoginId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 