IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginStatusClone')
BEGIN
	PRINT 'Dropping Procedure UserLoginStatusClone'
	DROP  Procedure UserLoginStatusClone
END
GO

PRINT 'Creating Procedure UserLoginStatusClone'
GO

/*********************************************************************************************
**		File: 
**		Name: UserLoginStatusClone
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

CREATE Procedure dbo.UserLoginStatusClone
(
		@UserLoginStatusId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserLoginStatus'
)
AS
BEGIN

	IF @UserLoginStatusId IS NULL OR @UserLoginStatusId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @UserLoginStatusId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.UserLoginStatus
	WHERE   UserLoginStatusId		=	@UserLoginStatusId
	ORDER BY UserLoginStatusId

	EXEC dbo.UserLoginStatusInsert 
			@UserLoginStatusId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLoginStatus'
		,	@EntityKey				= @UserLoginStatusId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
