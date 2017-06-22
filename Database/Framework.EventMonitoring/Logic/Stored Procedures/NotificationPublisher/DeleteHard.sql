IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherDeleteHard')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherDeleteHard'
	DROP  Procedure NotificationPublisherDeleteHard
END
GO

PRINT 'Creating Procedure NotificationPublisherDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: NotificationPublisherDelete
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
CREATE Procedure dbo.NotificationPublisherDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationPublisher'
)
AS
BEGIN

	IF @KeyType = 'NotificationPublisherId'
	BEGIN

		EXEC	dbo.NotificationPublisherDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'NotificationPublisherId',
				@AuditId	=	@AuditId

		DELETE	 dbo.NotificationPublisher
		WHERE	 NotificationPublisherId = @KeyId


	END

	
	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
