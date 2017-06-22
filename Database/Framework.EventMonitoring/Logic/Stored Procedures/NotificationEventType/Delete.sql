	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationEventTypeDelete')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeDelete'
	DROP  Procedure NotificationEventTypeDelete
END
GO

PRINT 'Creating Procedure NotificationEventTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: NotificationEventTypeDelete
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
CREATE Procedure dbo.NotificationEventTypeDelete
(
		@NotificationEventTypeId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'NotificationEventType'	
)
AS
BEGIN

	DELETE	 dbo.NotificationEventType
	WHERE	 NotificationEventTypeId = @NotificationEventTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationEventTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
