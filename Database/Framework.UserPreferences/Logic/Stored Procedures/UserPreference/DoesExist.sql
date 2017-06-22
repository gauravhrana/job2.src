IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceDoesExist')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDoesExist'
	DROP  Procedure  UserPreferenceDoesExist
END
GO

PRINT 'Creating Procedure UserPreferenceDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.UserPreferenceDoesExist
(
		@UserPreferenceId			INT				= NULL		
	,	@UserPreferenceCategoryId	INT							
	,	@UserPreferenceKeyId		INT
	,	@ApplicationUserId			INT							
	,	@ApplicationId				INT							
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'UserPreference'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.UserPreference a
	WHERE		a.ApplicationUserId			=	@ApplicationUserId	
	AND			a.UserPreferenceKeyId		=	@UserPreferenceKeyId
	AND			a.ApplicationId				=	@ApplicationId
	AND			a.UserPreferenceCategoryId	=	@UserPreferenceCategoryId


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

