IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceUpdateValueOnly')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceUpdateValueOnly'
	DROP  Procedure  UserPreferenceUpdateValueOnly
END
GO

PRINT 'Creating Procedure UserPreferenceUpdateValueOnly'
GO

/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceUpdateValueOnlyValueOnly
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

CREATE Procedure dbo.UserPreferenceUpdateValueOnly
(
		@UserPreferenceId			INT				
	,	@Value						VARCHAR(50)		
	,	@AuditId					INT				
	,	@AuditDate					DATETIME = NULL	
	,	@SystemEntityType			VARCHAR(50) = 'UserPreference'
)
AS
BEGIN

	UPDATE	dbo.UserPreference 
	SET		Value						=	@Value						
	WHERE	UserPreferenceId			=	@UserPreferenceId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END
 GO
