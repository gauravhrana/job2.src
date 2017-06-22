	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherDelete')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherDelete'
	DROP  Procedure NotificationPublisherDelete
END
GO

PRINT 'Creating Procedure NotificationPublisherDelete'
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
CREATE Procedure dbo.NotificationPublisherDelete
(
		@NotificationPublisherId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'NotificationPublisher'	
)
AS
BEGIN

	DELETE	 dbo.NotificationPublisher
	WHERE	 NotificationPublisherId = @NotificationPublisherId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationPublisherId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
