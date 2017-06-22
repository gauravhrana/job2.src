IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginUpdate')
BEGIN
	PRINT 'Dropping Procedure UserLoginUpdate'
	DROP  Procedure  UserLoginUpdate
END
GO

PRINT 'Creating Procedure UserLoginUpdate'
GO

/******************************************************************************
**		File: 
**		Name: UserLoginUpdate
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
**		Date:		Author:				RecordDate:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.UserLoginUpdate
(
		@UserLoginId				INT				
	,	@UserName					VARCHAR(50)						
	,	@UserLoginStatusId			INT						
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'UserLogin'
)
AS
BEGIN
	
	UPDATE	dbo.UserLogin 
	SET		UserName			=	@UserName				
		,	UserLoginStatusId	=	@UserLoginStatusId							
	WHERE	UserLoginId			=	@UserLoginId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLogin'
		,	@EntityKey				= @UserLoginId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO