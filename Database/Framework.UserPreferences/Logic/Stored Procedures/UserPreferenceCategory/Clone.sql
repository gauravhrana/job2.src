IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceCategoryClone')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryClone'
	DROP  Procedure UserPreferenceCategoryClone
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryClone'
GO

/*********************************************************************************************
**		Task: 
**		Name: UserPreferenceCategoryClone
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

CREATE Procedure dbo.UserPreferenceCategoryClone
(
		@UserPreferenceCategoryId		INT			= NULL 	OUTPUT
	,	@ApplicationId					INT			
	,	@Name							VARCHAR(100)						
	,	@Description					VARCHAR(500)						
	,	@SortOrder						INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50) = 'UserPreferenceCategory'			
)
AS
BEGIN

	IF @UserPreferenceCategoryId IS NULL OR @UserPreferenceCategoryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'UserPreferenceCategory', @UserPreferenceCategoryId OUTPUT
	END			
	
	SELECT	@Name				=	Name
		,	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.UserPreferenceCategory
	WHERE	UserPreferenceCategoryId		=	@UserPreferenceCategoryId

	EXEC dbo.UserPreferenceCategoryInsert 
			@UserPreferenceCategoryId		=	NULL
		,	@ApplicationId					=	@ApplicationId
		,	@Name							=	@Name
		,	@Description					=	@Description
		,	@SortOrder						=	@SortOrder
		,	@AuditId						=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@UserPreferenceCategoryId
		,	@AuditAction			=	'Clone'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId	

END	
GO
