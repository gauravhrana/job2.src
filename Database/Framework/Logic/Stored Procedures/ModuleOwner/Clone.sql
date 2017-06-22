IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleOwnerClone')
BEGIN
	PRINT 'Dropping Procedure ModuleOwnerClone'
	DROP  Procedure ModuleOwnerClone
END
GO

PRINT 'Creating Procedure ModuleOwnerClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ModuleOwnerClone
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

CREATE Procedure dbo.ModuleOwnerClone
(
		@ModuleOwnerId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@ModuleId				INT
	,	@DeveloperRoleId		INT					
	,	@Developer				VARCHAR(50)						
	,	@FeatureOwnerStatusId	INT										
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ModuleOwner'
)
AS
BEGIN

	IF @ModuleOwnerId IS NULL OR @ModuleOwnerId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ModuleOwnerId OUTPUT
	END						
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@Developer				=	Developer
		,	@DeveloperRoleId		=	DeveloperRoleId
		,	@FeatureOwnerStatusId	=	FeatureOwnerStatusId				
	FROM	dbo.ModuleOwner
	WHERE   ModuleOwnerId		=	@ModuleOwnerId
	ORDER BY ModuleOwnerId

	EXEC dbo.ModuleOwnerInsert 
			@ModuleOwnerId			=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@ModuleId				=	@ModuleId
		,	@DeveloperRoleId		=	@DeveloperRoleId
		,	@Developer				=	@Developer
		,	@FeatureOwnerStatusId	=	@FeatureOwnerStatusId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ModuleOwner'
		,	@EntityKey				= @ModuleOwnerId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
