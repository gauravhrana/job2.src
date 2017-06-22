IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginStatusUpdate')
BEGIN
	PRINT 'Dropping Procedure UserLoginStatusUpdate'
	DROP  Procedure  UserLoginStatusUpdate
END
GO

PRINT 'Creating Procedure UserLoginStatusUpdate'
GO

/******************************************************************************
**		File: 
**		Name: UserLoginStatusUpdate
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.UserLoginStatusUpdate
(
		@UserLoginStatusId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'UserLoginStatus'
)
AS
BEGIN
	UPDATE	dbo.UserLoginStatus 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	UserLoginStatusId	=	@UserLoginStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLoginStatus'
		,	@EntityKey				= @UserLoginStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO