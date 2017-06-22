IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationSubscriberList')
BEGIN
	PRINT 'Dropping Procedure NotificationSubscriberList'
	DROP  Procedure  dbo.NotificationSubscriberList
END
GO

PRINT 'Creating Procedure NotificationSubscriberList'
GO

/******************************************************************************
**		File: 
**		Name: NotificationSubscriberList
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
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.NotificationSubscriberList
(
		@AuditId				INT			
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationSubscriber'
)
AS
BEGIN

	SELECT	a.NotificationSubscriberId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
		
		
	FROM		dbo.NotificationSubscriber  a
	WHERE		a.ApplicationId = @ApplicationId
	ORDER BY	a.SortOrder	            ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END
GO

