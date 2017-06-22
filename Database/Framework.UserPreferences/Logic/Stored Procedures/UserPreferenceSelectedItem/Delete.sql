IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceSelectedItemDelete')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemDelete'
	DROP  Procedure UserPreferenceSelectedItemDelete
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemDelete'
GO


/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceSelectedItemDelete
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

CREATE Procedure dbo.UserPreferenceSelectedItemDelete
(
		@UserPreferenceSelectedItemId 	INT				= NULL
	,	@ParentKey						VARCHAR(50)		= NULL
	,	@UserPreferenceKeyId			INT				= NULL
	,	@AuditId						INT					
	,	@AuditDate						DATETIME		= NULL	
	,	@SystemEntityType				VARCHAR(50)		= 'UserPreferenceSelectedItem'
)
AS
BEGIN

	DELETE	dbo.UserPreferenceSelectedItem
	WHERE	UserPreferenceSelectedItemId	= ISNULL(@UserPreferenceSelectedItemId, UserPreferenceSelectedItemId)
	AND		ParentKey						= ISNULL(@ParentKey, ParentKey)
	AND		UserPreferenceKeyId				= ISNULL(@UserPreferenceKeyId, UserPreferenceKeyId)

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@UserPreferenceSelectedItemId
		,	@AuditAction			=	'Delete'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO

