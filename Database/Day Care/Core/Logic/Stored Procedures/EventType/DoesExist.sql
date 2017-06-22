IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='EventTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure EventTypeDoesExist'
	DROP  Procedure  EventTypeDoesExist
END
GO

PRINT 'Creating Procedure EventTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: EventTypeDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.EventTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL	
	,	@ApplicationId			INT		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'EventType'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.EventType a
	WHERE		a.Name = @Name
	AND			a.ApplicationId = @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert	
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

