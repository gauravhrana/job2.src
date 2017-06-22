IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceKeyDoesExist')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyDoesExist'
	DROP  Procedure  UserPreferenceKeyDoesExist
END
GO

PRINT 'Creating Procedure UserPreferenceKeyDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceKeyDoesExist
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

Create procedure dbo.UserPreferenceKeyDoesExist
(
		@UserPreferenceKeyId	INT				= NULL
	,	@ApplicationId			INT					
	,	@Name					VARCHAR(50)		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'UserPreferenceKey'	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.UserPreferenceKey a
	WHERE		a.Name			=	@Name	
	AND			a.ApplicationId	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceKeyId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

