IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryUpdate'
	DROP  Procedure  UserPreferenceCategoryUpdate
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryUpdate'
GO

/******************************************************************************
**		Task: 
**		Name: UserPreferenceCategoryUpdate
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

CREATE Procedure dbo.UserPreferenceCategoryUpdate
(
		@UserPreferenceCategoryId		INT		 				
	,	@Name							VARCHAR(100)					
	,	@Description					VARCHAR(500)				
	,	@SortOrder						INT						
	,	@AuditId						INT							
	,	@AuditDate						DATETIME	= NULL
	,	@SystemEntityType				VARCHAR(50) = 'UserPreferenceCategory'
)
AS
BEGIN 

	UPDATE	dbo.UserPreferenceCategory 
	SET		Name				=	@Name				
		,	Description			=	@Description				
		,	SortOrder			=	@SortOrder							
	WHERE	UserPreferenceCategoryId		=	@UserPreferenceCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @UserPreferenceCategoryId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO