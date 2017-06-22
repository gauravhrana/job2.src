IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeveloperRoleClone')
BEGIN
	PRINT 'Dropping Procedure DeveloperRoleClone'
	DROP  Procedure DeveloperRoleClone
END
GO

PRINT 'Creating Procedure DeveloperRoleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DeveloperRoleClone
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
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.DeveloperRoleClone
(
		@DeveloperRoleId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DeveloperRole'
)
AS
BEGIN

	IF @DeveloperRoleId IS NULL OR @DeveloperRoleId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DeveloperRoleId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.DeveloperRole
	WHERE   DeveloperRoleId		=	@DeveloperRoleId
	ORDER BY DeveloperRoleId

	EXEC dbo.DeveloperRoleInsert 
			@DeveloperRoleId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DeveloperRole'
		,	@EntityKey				= @DeveloperRoleId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
