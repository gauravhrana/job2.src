IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EventTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure EventTypeDeleteHard'
	DROP  Procedure EventTypeDeleteHard
END
GO

PRINT 'Creating Procedure EventTypeDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: EventTypeDeleteHard
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.EventTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'EventType'
)
AS
BEGIN

	IF @KeyType = 'EventTypeId'
	BEGIN 

		EXEC	dbo.CommentDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'EventTypeId'
			,	@AuditId 	=	@AuditId

		DELETE	 dbo.EventType
		WHERE	 EventTypeId = @KeyId

	END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
