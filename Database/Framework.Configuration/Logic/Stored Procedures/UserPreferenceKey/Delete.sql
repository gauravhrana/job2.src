IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyDelete')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyDelete'
	DROP  Procedure UserPreferenceKeyDelete
END

GO

PRINT 'Creating Procedure UserPreferenceKeyDelete'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceKeyDelete
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
CREATE Procedure dbo.UserPreferenceKeyDelete
(
		@UserPreferenceKeyId 	INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'UserPreferenceKey'
)
AS
BEGIN

	DELETE	 dbo.UserPreferenceKey
	WHERE	 UserPreferenceKeyId = @UserPreferenceKeyId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@UserPreferencekeyId
		,	@AuditAction			=	'Delete'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO


