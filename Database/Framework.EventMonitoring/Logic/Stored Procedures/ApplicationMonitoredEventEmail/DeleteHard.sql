IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationMonitoredEventEmailDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationMonitoredEventEmailDeleteHard'
	DROP  Procedure ApplicationMonitoredEventEmailDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationMonitoredEventEmailDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationMonitoredEventEmailDelete
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
CREATE Procedure dbo.ApplicationMonitoredEventEmailDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMonitoredEventEmail'
)
AS
BEGIN

	IF @KeyType = 'ApplicationMonitoredEventEmailId'
		BEGIN			

			DELETE	 dbo.ApplicationMonitoredEventEmail
			WHERE	 ApplicationMonitoredEventEmailId = @KeyId

		END
	ELSE IF	@KeyType = 'ApplicationMonitoredEventSourceId'
		BEGIN

			DELETE	 dbo.ApplicationMonitoredEventEmail
			WHERE	 ApplicationMonitoredEventSourceId = @KeyId

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
