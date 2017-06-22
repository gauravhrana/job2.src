IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryDelete'
	DROP  Procedure UserPreferenceCategoryDelete
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryDelete'
GO
/******************************************************************************
**		Task: 
**		Name: UserPreferenceCategoryDelete
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.UserPreferenceCategoryDelete
(
		@UserPreferenceCategoryId 	INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL	
	,	@SystemEntityType			VARCHAR(50) = 'UserPreferenceCategory'
)
AS
BEGIN

	DELETE	 dbo.UserPreferenceCategory
	WHERE	 UserPreferenceCategoryId = @UserPreferenceCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@UserPreferenceCategoryId
		,	@AuditAction			=	'Delete'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO
