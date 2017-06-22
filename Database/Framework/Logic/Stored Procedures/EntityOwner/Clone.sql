IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EntityOwnerClone')
BEGIN
	PRINT 'Dropping Procedure EntityOwnerClone'
	DROP  Procedure EntityOwnerClone
END
GO

PRINT 'Creating Procedure EntityOwnerClone'
GO

/*********************************************************************************************
**		File: 
**		Name: EntityOwnerClone
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Developer:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.EntityOwnerClone
(
		@EntityOwnerId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@EntityId				INT
	,	@DeveloperRoleId		INT					
	,	@Developer				VARCHAR(50)						
	,	@FeatureOwnerStatusId	INT										
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'EntityOwner'
)
AS
BEGIN

	IF @EntityOwnerId IS NULL OR @EntityOwnerId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @EntityOwnerId OUTPUT
	END						
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@Developer				=	Developer
		,	@DeveloperRoleId		=	DeveloperRoleId
		,	@FeatureOwnerStatusId	=	FeatureOwnerStatusId				
	FROM	dbo.EntityOwner
	WHERE   EntityOwnerId		=	@EntityOwnerId
	ORDER BY EntityOwnerId

	EXEC dbo.EntityOwnerInsert 
			@EntityOwnerId			=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@EntityId				=	@EntityId
		,	@DeveloperRoleId		=	@DeveloperRoleId
		,	@Developer				=	@Developer
		,	@FeatureOwnerStatusId	=	@FeatureOwnerStatusId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'EntityOwner'
		,	@EntityKey				= @EntityOwnerId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
