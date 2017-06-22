IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationRegistrarDetails')
BEGIN
  PRINT 'Dropping Procedure NotificationRegistrarDetails'
  DROP  Procedure NotificationRegistrarDetails
END

GO

PRINT 'Creating Procedure NotificationRegistrarDetails'
GO


/******************************************************************************
**		File: 
**		Name: NotificationRegistrarDetails
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
**		Date:		Author:				Developer:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.NotificationRegistrarDetails
(
		@NotificationRegistrarId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'NotificationRegistrar'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@NotificationRegistrarId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.NotificationRegistrarId			
		,	a.ApplicationId
		,	a.NotificationEventTypeId
		,	a.NotificationPublisherId
		,	a.Message
		,	a.PublishDateId		
		,	a.PublishTimeId
		,	b.Name					AS	'NotificationEventType'
		,	c.Name					AS	'NotificationPublisher'
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.NotificationRegistrar		a
	INNER JOIN	dbo.NotificationEventType			b
		ON	a.NotificationEventTypeId			=	b.NotificationEventTypeId
		INNER JOIN	dbo.NotificationPublisher			c
		ON	a.NotificationPublisherId			=	c.NotificationPublisherId
		WHERE	NotificationRegistrarId = @NotificationRegistrarId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'NotificationRegistrar'
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   