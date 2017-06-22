IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CommentList')
BEGIN
	PRINT 'Dropping Procedure CommentList'
	DROP PROCEDURE CommentList
END
GO

PRINT 'Creating Procedure CommentList'
GO

/******************************************************************************
**		File: 
**		Name: CommentList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.CommentList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Comment'
)
AS
BEGIN
		SELECT	a.CommentId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.Date	
			,	a.EventTypeId	
			,	a.Comment
		FROM   dbo.Comment  a
		WHERE  ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY CommentId	ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
