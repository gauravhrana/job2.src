IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateDetails'
	DROP  Procedure ApplicationMonitoredEventProcessingStateDetails
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateDetails
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

CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateDetails
(
		@ApplicationMonitoredEventProcessingStateId		INT					
	,	@AuditId										INT					
	,	@AuditDate										DATETIME	= NULL	
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationMonitoredEventProcessingStateId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	ApplicationMonitoredEventProcessingStateId	
		,	ApplicationId	
		,	Code													
		,	Description		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'	
	FROM	dbo.ApplicationMonitoredEventProcessingState 
	WHERE	ApplicationMonitoredEventProcessingStateId = @ApplicationMonitoredEventProcessingStateId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventProcessingStateId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   