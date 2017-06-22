IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EntityOwnerInsert')
BEGIN
	PRINT 'Dropping Procedure EntityOwnerInsert'
	DROP  Procedure EntityOwnerInsert
END
GO

PRINT 'Creating Procedure EntityOwnerInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:EntityOwnerInsert
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

CREATE Procedure dbo.EntityOwnerInsert
(
		@EntityOwnerId				INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT
	,	@EntityId					INT
	,	@DeveloperRoleId			INT					
	,	@Developer					VARCHAR(50)						
	,	@FeatureOwnerStatusId		INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'EntityOwner'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @EntityOwnerId OUTPUT, @AuditId
	
	INSERT INTO dbo.EntityOwner 
	( 
			EntityOwnerId	
		,   ApplicationId					
		,	EntityId
		,	DeveloperRoleId						
		,	Developer					
		,	FeatureOwnerStatusId						
	)
	VALUES 
	(  
			@EntityOwnerId	
		,   @ApplicationId	
		,	@EntityId	
		,	@DeveloperRoleId					
		,	@Developer				
		,	@FeatureOwnerStatusId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @EntityOwnerId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 