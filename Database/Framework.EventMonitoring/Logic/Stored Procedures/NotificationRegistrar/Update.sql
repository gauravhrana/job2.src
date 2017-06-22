IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationRegistrarUpdate')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarUpdate'
	DROP  Procedure  NotificationRegistrarUpdate
END
GO

PRINT 'Creating Procedure NotificationRegistrarUpdate'
GO

/******************************************************************************
**		File: 
**		Name: NotificationRegistrarUpdate
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

CREATE Procedure dbo.NotificationRegistrarUpdate
(
		@NotificationRegistrarId		INT		 				
	,	@NotificationEventTypeId		INT						
	,	@NotificationPublisherId		INT						
	,	@ApplicationId									INT						
	,	@Message					VARCHAR(255)   	
	,	@AuditId								INT							
	,	@AuditDate								DATETIME	= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'NotificationRegistrar'
)
AS
BEGIN 

	DECLARE @Date DATETIME = Getdate()
	DECLARE @PublishDateId INT = CONVERT(VARCHAR(30), @Date, 112)
	DECLARE @PublishTimeId INT = Replace(convert(varchar(5),@Date,108),':','')+'00'

	UPDATE	dbo.NotificationRegistrar 
	SET		NotificationEventTypeId	=	@NotificationEventTypeId 
		,	NotificationPublisherId								=	@NotificationPublisherId											
		,	ApplicationId 								=	@ApplicationId 
		,	Message					=	@Message
		,	PublishDateId								=	@PublishDateId	
		,	PublishTimeId								=	@PublishTimeId																					
	WHERE	NotificationRegistrarId	=	@NotificationRegistrarId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

