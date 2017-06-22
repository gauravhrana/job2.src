IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='NotificationEventTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeDoesExist'
	DROP  Procedure  NotificationEventTypeDoesExist
END
GO

PRINT 'Creating Procedure NotificationEventTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: NotificationEventTypeDoesExist
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

Create procedure dbo.NotificationEventTypeDoesExist
(
		@NotificationEventTypeId			INT	
	,	@ApplicationId							INT	
	,	@Name									VARCHAR(50)		= NULL		
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationEventType'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.NotificationEventType a
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

