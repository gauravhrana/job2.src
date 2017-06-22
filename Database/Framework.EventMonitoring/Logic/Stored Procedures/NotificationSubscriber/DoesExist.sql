IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='NotificationSubscriberDoesExist')
BEGIN
	PRINT 'Dropping Procedure NotificationSubscriberDoesExist'
	DROP  Procedure  NotificationSubscriberDoesExist
END
GO

PRINT 'Creating Procedure NotificationSubscriberDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: NotificationSubscriberDoesExist
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

Create procedure dbo.NotificationSubscriberDoesExist
(
		@NotificationSubscriberId			INT	
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationSubscriber'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.NotificationSubscriber a
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

