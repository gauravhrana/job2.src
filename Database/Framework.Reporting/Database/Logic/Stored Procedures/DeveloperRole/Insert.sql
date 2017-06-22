IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeveloperRoleInsert')
BEGIN
	PRINT 'Dropping Procedure DeveloperRoleInsert'
	DROP  Procedure DeveloperRoleInsert
END
GO

PRINT 'Creating Procedure DeveloperRoleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:DeveloperRoleInsert
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

CREATE Procedure dbo.DeveloperRoleInsert
(
		@DeveloperRoleId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'DeveloperRole'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DeveloperRoleId OUTPUT, @AuditId
	
	INSERT INTO dbo.DeveloperRole 
	( 
			DeveloperRoleId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@DeveloperRoleId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DeveloperRoleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 