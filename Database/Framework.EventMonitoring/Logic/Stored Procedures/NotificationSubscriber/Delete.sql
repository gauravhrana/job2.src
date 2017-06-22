	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationSubscriberDelete')
BEGIN
	PRINT 'Dropping Procedure NotificationSubscriberDelete'
	DROP  Procedure NotificationSubscriberDelete
END
GO

PRINT 'Creating Procedure NotificationSubscriberDelete'
GO
/******************************************************************************
**		File: 
**		Name: NotificationSubscriberDelete
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
CREATE Procedure dbo.NotificationSubscriberDelete
(
		@NotificationSubscriberId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'NotificationSubscriber'	
)
AS
BEGIN

	DELETE	 dbo.NotificationSubscriber
	WHERE	 NotificationSubscriberId = @NotificationSubscriberId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationSubscriberId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
