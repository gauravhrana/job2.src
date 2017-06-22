IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventProcessingStateDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventProcessingStateDeleteHard'
	DROP  Procedure ApplicationMonitoredEventProcessingStateDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventProcessingStateDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventProcessingStateDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventProcessingStateDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMonitoredEventProcessingState'
)
AS
BEGIN

	IF @KeyType = 'ApplicationMonitoredEventProcessingStateId'
	BEGIN	

		EXEC	dbo.ApplicationMonitoredEventDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'ApplicationMonitoredEventProcessingStateId',
				@AuditId	=	@AuditId

		DELETE	 dbo.ApplicationMonitoredEventProcessingState
		WHERE	 ApplicationMonitoredEventProcessingStateId = @KeyId

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
