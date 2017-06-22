IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationPublisherList')
BEGIN
	PRINT 'Dropping Procedure NotificationPublisherList'
	DROP  Procedure  dbo.NotificationPublisherList
END
GO

PRINT 'Creating Procedure NotificationPublisherList'
GO

/******************************************************************************
**		File: 
**		Name: NotificationPublisherList
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

CREATE Procedure dbo.NotificationPublisherList
(
		@AuditId				INT			
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationPublisher'
)
AS
BEGIN

	SELECT	a.NotificationPublisherId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
		
		
	FROM		dbo.NotificationPublisher  a
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

