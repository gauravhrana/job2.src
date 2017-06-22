IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleInsert'
	DROP  Procedure ApplicationOperationXApplicationRoleInsert
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationOperationXApplicationRoleInsert
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

CREATE Procedure dbo.ApplicationOperationXApplicationRoleInsert
(
		@ApplicationOperationXApplicationRoleId		INT			= NULL 	OUTPUT	
	,	@ApplicationId								INT			= NULL	
	,	@ApplicationOperationId						INT								
	,	@ApplicationRoleId							INT								
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'ApplicationOperationXApplicationRole'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationOperationXApplicationRoleId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationOperationXApplicationRole 
	( 
			ApplicationOperationXApplicationRoleId		
		,	ApplicationId			
		,	ApplicationOperationId					
		,	ApplicationRoleId						
	)
	VALUES 
	(  
			@ApplicationOperationXApplicationRoleId		
		,	@ApplicationId			
		,	@ApplicationOperationId			
		,	@ApplicationRoleId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationXApplicationRoleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 