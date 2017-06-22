IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherXEventTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherXEventTypeDeleteHard'
	DROP  Procedure NotificationPublisherXEventTypeDeleteHard
END
GO

PRINT 'Creating Procedure NotificationPublisherXEventTypeDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: NotificationPublisherXEventTypeDelete
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
CREATE Procedure dbo.NotificationPublisherXEventTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'NotificationPublisherXEventType'	 
)
AS
BEGIN

	IF @KeyType = 'NotificationPublisherXEventTypeId'
		BEGIN

			DELETE	 dbo.NotificationPublisherXEventType
			WHERE	 NotificationPublisherXEventTypeId = @KeyId

		END
	ELSE IF @KeyType = 'NotificationEventType'
		BEGIN

			DELETE	 dbo.NotificationPublisherXEventType
			WHERE	 NotificationEventTypeId = @KeyId

		END
	ELSE IF @KeyType = 'NotificationPublisherId'
		BEGIN

			DELETE	 dbo.NotificationPublisherXEventType
			WHERE	 NotificationPublisherId = @KeyId

		END
	ELSE IF @KeyType = 'ApplicationId'
		BEGIN

			DELETE	 dbo.NotificationPublisherXEventType
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
