IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleInsert'
	DROP  Procedure ApplicationRoleInsert
END
GO

PRINT 'Creating Procedure ApplicationRoleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationRoleInsert
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

CREATE Procedure dbo.ApplicationRoleInsert
(
		@ApplicationRoleId		INT			= NULL 	OUTPUT		
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@ApplicationId			INT								
	,	@AuditId				INT								
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRole'	
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationRoleId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationRole 
	( 
			ApplicationRoleId						
		,	Name					
		,	Description				
		,	SortOrder				
		,	ApplicationId		
	)
	VALUES 
	(  
			@ApplicationRoleId	
		,	@Name				
		,	@Description		
		,	@SortOrder			
		,	@ApplicationId
	)

	SELECT @ApplicationRoleId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationRoleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO