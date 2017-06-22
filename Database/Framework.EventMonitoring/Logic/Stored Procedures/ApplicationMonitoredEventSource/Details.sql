IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceDetails'
	DROP  Procedure ApplicationMonitoredEventSourceDetails
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceDetails
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

CREATE Procedure dbo.ApplicationMonitoredEventSourceDetails
(
		@ApplicationMonitoredEventSourceId		INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationMonitoredEventSourceId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	ApplicationMonitoredEventSourceId	
		,	ApplicationId	
		,	Code											
		,	Description		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'				
	FROM	dbo.ApplicationMonitoredEventSource 
	WHERE	ApplicationMonitoredEventSourceId = @ApplicationMonitoredEventSourceId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventSourceId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   