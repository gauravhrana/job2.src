IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserInsert'
	DROP  Procedure ApplicationUserInsert
END
GO

PRINT 'Creating Procedure ApplicationUserInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationUserInsert
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

CREATE Procedure dbo.ApplicationUserInsert
(
		@ApplicationUserId	INT			= NULL 	OUTPUT	
	,	@FirstName			VARCHAR(50)					
	,	@LastName			VARCHAR(50)					
	,	@MiddleName			VARCHAR(50)							
	,	@AuditId			INT							
	,	@AuditDate			DATETIME	= NULL			
	,	@SystemEntityType	VARCHAR(50)	= 'ApplicationUser'			
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationUserId OUTPUT, @AuditId
		
	INSERT INTO dbo.ApplicationUser 
	( 
			ApplicationUserId						
		,	FirstName		
		,	LastName		
		,	MiddleName 							
	)
	VALUES 
	(  
			@ApplicationUserId		
		,	@FirstName		
		,	@LastName		
		,	@MiddleName				
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType			= @SystemEntityType	
		,	@EntityKey					= @ApplicationUserId
		,	@AuditAction				= 'Insert' 
		,	@CreatedDate				= @AuditDate
		,	@CreatedByPersonId	= @AuditId	
END	
GO
