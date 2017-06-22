IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationEventTypeList')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeList'
	DROP  Procedure  dbo.NotificationEventTypeList
END
GO

PRINT 'Creating Procedure NotificationEventTypeList'
GO

/******************************************************************************
**		File: 
**		Name: NotificationEventTypeList
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

CREATE Procedure dbo.NotificationEventTypeList
(
		@AuditId				INT			
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationEventType'
)
AS
BEGIN

	SELECT	a.NotificationEventTypeId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
		
		
	FROM		dbo.NotificationEventType  a
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

