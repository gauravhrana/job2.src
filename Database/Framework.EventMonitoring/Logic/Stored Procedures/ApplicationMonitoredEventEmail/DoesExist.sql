IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationMonitoredEventEmailDoesExist')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailDoesExist'
	DROP  Procedure  ApplicationMonitoredEventEmailDoesExist
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailDoesExist
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

Create procedure dbo.ApplicationMonitoredEventEmailDoesExist
(
		@ApplicationMonitoredEventSourceId		INT							
	,	@UserId									INT							
	,	@CorrespondenceLevel					VARCHAR(20)					
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationMonitoredEventEmail'
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.ApplicationMonitoredEventEmail a
	WHERE		a.ApplicationMonitoredEventSourceId	=	@ApplicationMonitoredEventSourceId
	AND			a.UserId							=	@UserId
	AND			a.CorrespondenceLevel				=	@CorrespondenceLevel

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

