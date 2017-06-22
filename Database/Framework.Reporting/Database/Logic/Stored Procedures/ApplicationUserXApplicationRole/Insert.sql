IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleInsert'
	DROP  Procedure ApplicationUserXApplicationRoleInsert
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationUserXApplicationRoleInsert
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

CREATE Procedure dbo.ApplicationUserXApplicationRoleInsert
(
		@ApplicationUserXApplicationRoleId		INT			= NULL 	OUTPUT	
	,	@ApplicationId							INT			
	,	@ApplicationUserId						INT								
	,	@ApplicationRoleId						INT								
	,	@AuditId								INT			= NULL								
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserXApplicationRole'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationUserXApplicationRoleId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationUserXApplicationRole 
	( 
			ApplicationUserXApplicationRoleId
		,	ApplicationId						
		,	ApplicationUserId					
		,	ApplicationRoleId						
	)
	VALUES 
	(  
			@ApplicationUserXApplicationRoleId	
		,	@ApplicationId					
		,	@ApplicationUserId			
		,	@ApplicationRoleId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserXApplicationRoleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 