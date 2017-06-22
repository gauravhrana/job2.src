IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='NotificationPublisherDoesExist')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherDoesExist'
	DROP  Procedure  NotificationPublisherDoesExist
END
GO

PRINT 'Creating Procedure NotificationPublisherDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherDoesExist
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

Create procedure dbo.NotificationPublisherDoesExist
(
		@NotificationPublisherId			INT	
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationPublisher'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.NotificationPublisher a
	WHERE		a.Name			=	@Name 
	AND			a.ApplicationId	=	@ApplicationId	
		

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

