IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginHistoryInsert')
BEGIN
	PRINT 'Dropping Procedure UserLoginHistoryInsert'
	DROP  Procedure UserLoginHistoryInsert
END
GO

PRINT 'Creating Procedure UserLoginHistoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:UserLoginHistoryInsert
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

CREATE Procedure dbo.UserLoginHistoryInsert
(
		@UserLoginHistoryId			INT				= NULL 	OUTPUT	
	,   @ApplicationId			INT				= NULL	
	,	@UserName				VARCHAR(50)	 				
	,	@URL					VARCHAR(200)	
	,	@ServerName					VARCHAR(50)	 						 						
	,	@UserId					INT								
	,	@Date					DateTime								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'UserLoginHistory'
)
AS	
BEGIN    

		
	INSERT INTO dbo.UserLoginHistory 
	( 
			ApplicationId					
		,	UserName						
		,	DateVisited					
		,	UserId	
		,	URL
		,	ServerName
							
	)
	VALUES 
	(  
			@ApplicationId	
		,	@UserName						
		,	@Date				
		,	@UserId	
		,	@URL
		,	@ServerName		
	)

	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserLoginHistoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 