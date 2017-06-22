IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventSourceDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventSourceDeleteHard'
	DROP  Procedure ApplicationMonitoredEventSourceDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventSourceDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventSourceDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventSourceDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMonitoredEventSource'
)
AS
BEGIN

	IF @KeyType = 'ApplicationMonitoredEventSourceId'
	BEGIN	

		EXEC	dbo.ApplicationMonitoredEventDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'ApplicationMonitoredEventSourceId',
				@AuditId	=	@AuditId

		EXEC	dbo.ApplicationMonitoredEventEmailDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'ApplicationMonitoredEventSourceId',
				@AuditId	=	@AuditId

		DELETE	 dbo.ApplicationMonitoredEventSource
		WHERE	 ApplicationMonitoredEventSourceId = @KeyId

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
