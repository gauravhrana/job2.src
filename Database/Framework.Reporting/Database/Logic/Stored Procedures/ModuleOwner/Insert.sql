IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleOwnerInsert')
BEGIN
	PRINT 'Dropping Procedure ModuleOwnerInsert'
	DROP  Procedure ModuleOwnerInsert
END
GO

PRINT 'Creating Procedure ModuleOwnerInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ModuleOwnerInsert
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
**		Date:		Author:				Developer:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.ModuleOwnerInsert
(
		@ModuleOwnerId				INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT
	,	@ModuleId					INT
	,	@DeveloperRoleId			INT					
	,	@Developer					VARCHAR(50)						
	,	@FeatureOwnerStatusId		INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'ModuleOwner'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ModuleOwnerId OUTPUT, @AuditId
	
	INSERT INTO dbo.ModuleOwner 
	( 
			ModuleOwnerId	
		,   ApplicationId					
		,	ModuleId
		,	DeveloperRoleId						
		,	Developer					
		,	FeatureOwnerStatusId						
	)
	VALUES 
	(  
			@ModuleOwnerId	
		,   @ApplicationId	
		,	@ModuleId	
		,	@DeveloperRoleId					
		,	@Developer				
		,	@FeatureOwnerStatusId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ModuleOwnerId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 