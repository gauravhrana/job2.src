IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CommentInsert')
BEGIN
	PRINT 'Dropping Procedure CommentInsert'
	DROP  Procedure  CommentInsert
END
GO

PRINT 'Creating Procedure CommentInsert'
GO

/******************************************************************************
**		File: 
**		Name: pCommentInsert
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
CREATE Procedure dbo.CommentInsert
(
		@CommentId		    INT
	,	@ApplicationId		INT
	,	@StudentId			INT
	,	@Date               DATETIME  
	,	@EventTypeId		INT
	,	@Comment			VARCHAR(500)
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'Comment'	
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CommentId OUTPUT
	

	INSERT INTO dbo.Comment
	(
			 CommentId
		,	 ApplicationId
        ,    StudentId
		,    Date
		,	 EventTypeId
		,	 Comment
	)
	VALUES
	(
			@CommentId
		,	@ApplicationId
		,	@StudentId
		,	@Date
		,	@EventTypeId
		,	@Comment
	)

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CommentId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	
END
