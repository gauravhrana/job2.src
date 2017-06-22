IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventDetails'
	DROP  Procedure ApplicationMonitoredEventDetails
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventDetails
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

CREATE Procedure dbo.ApplicationMonitoredEventDetails
(
		@ApplicationMonitoredEventId		INT					
	,	@AuditId							INT					
	,	@AuditDate							DATETIME		= NULL	
	,	@SystemEntityType					VARCHAR(50)		= 'ApplicationMonitoredEvent'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationMonitoredEventId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	a.ApplicationMonitoredEventId
		,	a.ApplicationMonitoredEventSourceId			
		,	a.ApplicationMonitoredEventProcessingStateId	
		,	a.ReferenceId									
		,	a.ReferenceCode								
		,	a.Category	
		,	a.Message 		
		,	a.IsDuplicate		
		,	a.LastModifiedBy	
		,	a.LastModifiedOn											
		,	b.Code					AS	'ApplicationMonitoredEventSource'
		,	c.Code					AS	'ApplicationMonitoredEventProcessingState'					
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.ApplicationMonitoredEvent					a
	INNER JOIN	dbo.ApplicationMonitoredEventSource				b		ON		a.ApplicationMonitoredEventSourceId				= b.ApplicationMonitoredEventSourceId
	INNER JOIN	dbo.ApplicationMonitoredEventProcessingState	c		ON		a.ApplicationMonitoredEventProcessingStateId	= c.ApplicationMonitoredEventProcessingStateId	
	WHERE	ApplicationMonitoredEventId = @ApplicationMonitoredEventId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   