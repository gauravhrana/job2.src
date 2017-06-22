IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationRegistrarList')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarList'
	DROP  Procedure  dbo.NotificationRegistrarList
END
GO

PRINT 'Creating Procedure NotificationRegistrarList'
GO

/******************************************************************************
**		File: 
**		Name: NotificationRegistrarList
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Developer:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.NotificationRegistrarList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationRegistrar'
)
AS
BEGIN

	SELECT	a.NotificationRegistrarId			
		,	a.ApplicationId
		,	a.NotificationEventTypeId
		,	a.NotificationPublisherId
		,	a.Message			
		,	a.PublishDateId			
		,	a.PublishTimeId			
		,	b.Name					AS	'NotificationEventType'
		,	c.Name					AS	'NotificationPublisher'

	FROM		dbo.NotificationRegistrar		a
	INNER JOIN	dbo.NotificationEventType			b
		ON	a.NotificationEventTypeId			=	b.NotificationEventTypeId
		INNER JOIN	dbo.NotificationPublisher			c
		ON	a.NotificationPublisherId			=	c.NotificationPublisherId	
	 WHERE	a.ApplicationId		=	@ApplicationId
	ORDER BY a.NotificationRegistrarId			ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO