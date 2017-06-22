IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceCategoryDoesExist')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryDoesExist'
	DROP  Procedure  UserPreferenceCategoryDoesExist
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryDoesExist'
GO

/******************************************************************************
**		Task: 
**		Name: UserPreferenceCategoryDoesExist
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

Create procedure dbo.UserPreferenceCategoryDoesExist
(
		@UserPreferenceCategoryId		INT				= NULL
	,	@ApplicationId					INT	
	,	@Name							VARCHAR(100)		
	,	@AuditId						INT							
	,	@AuditDate						DATETIME		= NULL		
	,	@SystemEntityType				VARCHAR(50)		= 'UserPreferenceCategory'	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.UserPreferenceCategory a
	WHERE		a.Name			=	@Name	
	AND			a.ApplicationId	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceCategoryId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

