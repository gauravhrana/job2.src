IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventDeleteHard'
	DROP  Procedure ApplicationMonitoredEventDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ApplicationMonitoredEvent'
)
AS
BEGIN

	IF @KeyType = 'ApplicationMonitoredEventId'
		BEGIN			

			DELETE	 dbo.ApplicationMonitoredEvent
			WHERE	 ApplicationMonitoredEventId = @KeyId

		END
	ELSE IF	@KeyType = 'ApplicationMonitoredEventSourceId'
		BEGIN

			DELETE	 dbo.ApplicationMonitoredEvent
			WHERE	 ApplicationMonitoredEventSourceId = @KeyId

		END
	ELSE IF	@KeyType = 'ApplicationMonitoredEventProcessingStateId'
		BEGIN

			DELETE	 dbo.ApplicationMonitoredEvent
			WHERE	 ApplicationMonitoredEventProcessingStateId = @KeyId

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
