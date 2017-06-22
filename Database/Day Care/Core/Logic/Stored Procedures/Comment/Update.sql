IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CommentUpdate')
BEGIN
	PRINT 'Dropping Procedure CommentUpdate'
	DROP  Procedure  CommentUpdate
END
GO

PRINT 'Creating Procedure CommentUpdate'
GO

/******************************************************************************
**		File: 
**		Name: CommentUpdate
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
**		----------						-----------
**
**		Auth:
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.CommentUpdate
(      
		@CommentId		    INT		
	,	@StudentId			INT
	,   @Date               DATETIME  
	,	@EventTypeId		INT
	,	@Comment			VARCHAR(500)
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL 
	,	@SystemEntityType	VARCHAR(50)	= 'Comment'	
)
AS
 BEGIN
	UPDATE	 dbo.Comment
	SET		 StudentId			=	@StudentId
		,    Date               =   @Date
		,	 EventTypeId		=	@EventTypeId
		,	 Comment			=	@Comment
	WHERE	 CommentId		    =   @CommentId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CommentId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END
GO

