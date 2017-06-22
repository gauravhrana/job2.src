IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceSelectedItemUpdate')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemUpdate'
	DROP  Procedure  UserPreferenceSelectedItemUpdate
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemUpdate'
GO

/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceSelectedItemUpdate
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

CREATE Procedure dbo.UserPreferenceSelectedItemUpdate
(
		@UserPreferenceSelectedItemId		INT		
	,	@ApplicationUserId					INT				
	,	@UserPreferenceKeyId				INT				
	,	@ParentKey							VARCHAR(50)	
	,	@Value								VARCHAR(50)	
	,	@SortOrder							INT			
	,	@AuditId							INT				
	,	@AuditDate							DATETIME = NULL	
	,	@SystemEntityType					VARCHAR(50) = 'UserPreferenceSelectedItem'
)
AS
BEGIN

	UPDATE	dbo.UserPreferenceSelectedItem 
	SET		ApplicationUserId				=	@ApplicationUserId		
		,	UserPreferenceKeyId				=	@UserPreferenceKeyId		
		,	ParentKey						=	@ParentKey				
		,	Value							=	@Value					
		,	SortOrder						=	@SortOrder				
	WHERE	UserPreferenceSelectedItemId	=	@UserPreferenceSelectedItemId	
		
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceSelectedItemId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
 GO
