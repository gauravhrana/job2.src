IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CommentDetails')
BEGIN
	PRINT 'Dropping Procedure CommentDetails'
	DROP  Procedure CommentDetails
END
GO

PRINT 'Creating Procedure CommentDetails'
GO


/******************************************************************************
**		File: 
**		Name: CommentDetails
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

CREATE Procedure dbo.CommentDetails
(
		@CommentId		    INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Comment'	
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@CommentId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	CommentId
		,	ApplicationId
        ,   StudentId
        ,	Date
		,	EventTypeId
		,	Comment	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	dbo.Comment 
	WHERE	CommentId = @CommentId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @CommentId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO
   