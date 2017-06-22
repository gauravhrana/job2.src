IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationMonitoredEventSourceDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceDoesExist'
	DROP  Procedure  ApplicationMonitoredEventSourceDoesExist
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.ApplicationMonitoredEventSourceDoesExist
(
		@Code					VARCHAR(20)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.ApplicationMonitoredEventSource a
	WHERE		a.Code = @Code	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

