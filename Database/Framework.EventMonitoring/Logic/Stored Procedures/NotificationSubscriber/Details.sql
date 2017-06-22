IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationSubscriberDetails')
BEGIN
	PRINT 'Dropping Procedure NotificationSubscriberDetails'
	DROP  Procedure NotificationSubscriberDetails
END
GO

PRINT 'Creating Procedure NotificationSubscriberDetails'
GO


/******************************************************************************
**		File: 
**		Name: NotificationSubscriberDetails
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

CREATE Procedure dbo.NotificationSubscriberDetails
(
		@NotificationSubscriberId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'NotificationSubscriber'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@NotificationSubscriberId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.NotificationSubscriberId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.NotificationSubscriber a
	
	WHERE	a.NotificationSubscriberId	= @NotificationSubscriberId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationSubscriberId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   