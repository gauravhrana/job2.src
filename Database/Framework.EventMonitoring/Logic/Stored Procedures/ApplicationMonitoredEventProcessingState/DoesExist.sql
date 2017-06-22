IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationMonitoredEventProcessingStateDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateDoesExist'
	DROP  Procedure  ApplicationMonitoredEventProcessingStateDoesExist
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateDoesExist
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

Create procedure dbo.ApplicationMonitoredEventProcessingStateDoesExist
(
		@Code					VARCHAR(20)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.ApplicationMonitoredEventProcessingState a
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

