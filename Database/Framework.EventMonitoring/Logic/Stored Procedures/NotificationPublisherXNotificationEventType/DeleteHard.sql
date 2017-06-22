IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXNotificationEventTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXNotificationEventTypeDeleteHard'
	DROP  Procedure NotificationPublisherXNotificationEventTypeDeleteHard
END
GO

PRINT 'Creating Procedure NotificationPublisherXNotificationEventTypeDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: NotificationPublisherXNotificationEventTypeDelete
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
CREATE Procedure dbo.NotificationPublisherXNotificationEventTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'NotificationPublisherXNotificationEventType'	 
)
AS
BEGIN

	IF @KeyType = 'NotificationPublisherXNotificationEventTypeId'
		BEGIN

			DELETE	 dbo.NotificationPublisherXNotificationEventType
			WHERE	 NotificationPublisherXNotificationEventTypeId = @KeyId

		END
	ELSE IF @KeyType = 'NotificationEventType'
		BEGIN

			DELETE	 dbo.NotificationPublisherXNotificationEventType
			WHERE	 NotificationEventTypeId = @KeyId

		END
	ELSE IF @KeyType = 'NotificationPublisherId'
		BEGIN

			DELETE	 dbo.NotificationPublisherXNotificationEventType
			WHERE	 NotificationPublisherId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationId'
		BEGIN

			DELETE	 dbo.NotificationPublisherXNotificationEventType
			WHERE	 NotificationEventTypeId IN
			(
				SELECT NotificationEventTypeId
				FROM  dbo. dbo.NotificationEventType 
				WHERE NotificationEventTypeId = @KeyId	
			)
	END
	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
