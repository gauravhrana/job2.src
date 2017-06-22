IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationEventTypeDetails')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeDetails'
	DROP  Procedure NotificationEventTypeDetails
END
GO

PRINT 'Creating Procedure NotificationEventTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: NotificationEventTypeDetails
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

CREATE Procedure dbo.NotificationEventTypeDetails
(
		@NotificationEventTypeId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'NotificationEventType'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@NotificationEventTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.NotificationEventTypeId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.NotificationEventType a
	
	WHERE	a.NotificationEventTypeId	= @NotificationEventTypeId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationEventTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   