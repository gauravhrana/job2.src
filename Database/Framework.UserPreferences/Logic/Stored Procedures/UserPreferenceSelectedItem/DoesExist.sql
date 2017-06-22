IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceSelectedItemDoesExist')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemDoesExist'
	DROP  Procedure  UserPreferenceSelectedItemDoesExist
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceSelectedItemDoesExist
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

Create procedure dbo.UserPreferenceSelectedItemDoesExist
(
		@UserPreferenceSelectedItemId			INT				= NULL	
	,	@ApplicationUserId						INT				
	,	@ParentKey								VARCHAR(50)							
	,	@Value									VARCHAR(50)	
	,	@ApplicationId							INT							
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'UserPreferenceSelectedItem'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.UserPreferenceSelectedItem a
	WHERE		a.ApplicationUserId						=	@ApplicationUserId	
	AND			a.ParentKey								=	@ParentKey
	AND			a.Value									=	@Value
	AND			a.ApplicationId							=	@ApplicationId


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceSelectedItemId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

