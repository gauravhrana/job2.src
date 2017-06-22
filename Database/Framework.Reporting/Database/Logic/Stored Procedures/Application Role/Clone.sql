IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleClone'
	DROP  Procedure ApplicationRoleClone
END
GO

PRINT 'Creating Procedure ApplicationRoleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationRoleClone
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

CREATE Procedure dbo.ApplicationRoleClone
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
	
	SELECT	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder	
		,	@ApplicationId		= ApplicationId			
	FROM	dbo.ApplicationRole
	WHERE	ApplicationRoleId	= @ApplicationRoleId
	ORDER BY ApplicationRoleId

	EXEC dbo.ApplicationRoleInsert 
			@ApplicationRoleId	=	NULL
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId
		,	@ApplicationId		=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationRoleId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO