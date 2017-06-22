IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CommentDelete')
BEGIN
	PRINT 'Dropping Procedure CommentDelete'
	DROP  Procedure  CommentDelete
END
GO

PRINT 'Creating Procedure CommentDelete'
GO

/******************************************************************************
**		File: 
**		Name: CommentDelete
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

CREATE Procedure dbo.CommentDelete
(
	    @CommentId			INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Comment'	
)
AS
BEGIN
	DELETE	dbo.Comment
	WHERE	CommentId = @CommentId

--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @CommentId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END
GO

