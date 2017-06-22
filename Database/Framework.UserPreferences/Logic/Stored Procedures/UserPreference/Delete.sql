IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDelete')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDelete'
	DROP  Procedure UserPreferenceDelete
END
GO

PRINT 'Creating Procedure UserPreferenceDelete'
GO


/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceDelete
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

CREATE Procedure dbo.UserPreferenceDelete
(
		@UserPreferenceId 		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'UserPreference'
)
AS
BEGIN

	DELETE	 dbo.UserPreference
	WHERE	 UserPreferenceId = @UserPreferenceId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@UserPreferenceId
		,	@AuditAction			=	'Delete'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO

