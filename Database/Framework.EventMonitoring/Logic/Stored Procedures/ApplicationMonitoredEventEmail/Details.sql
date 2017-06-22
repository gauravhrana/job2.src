IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventEmailDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailDetails'
	DROP  Procedure ApplicationMonitoredEventEmailDetails
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailDetails'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailDetails
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

CREATE Procedure dbo.ApplicationMonitoredEventEmailDetails
(
		@ApplicationMonitoredEventEmailId		INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventEmail'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationMonitoredEventEmailId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	a.ApplicationMonitoredEventEmailId 
		,	a.ApplicationMonitoredEventSourceId
		,	a.UserId 
		,	a.CorrespondenceLevel
		,	a.Active
		,	b.Code						AS	'ApplicationMonitoredEventSource'
		,	c.FirstName + c.LastName	AS	'User'					
		,	@LastUpdatedDate			AS	'UpdatedDate'
		,	@LastUpdatedBy				AS	'UpdatedBy'
		,	@LastAuditAction			AS	'LastAction'					
	FROM		dbo.ApplicationMonitoredEventEmail		a
	INNER JOIN	dbo.ApplicationMonitoredEventSource		b		ON		a.ApplicationMonitoredEventSourceId		= b.ApplicationMonitoredEventSourceId
	INNER JOIN	dbo.Person								c		ON		a.UserId								= c.PersonId
	WHERE	ApplicationMonitoredEventEmailId = @ApplicationMonitoredEventEmailId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationMonitoredEventEmailId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   