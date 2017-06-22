IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationEventTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeDeleteHard'
	DROP  Procedure NotificationEventTypeDeleteHard
END
GO

PRINT 'Creating Procedure NotificationEventTypeDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: NotificationEventTypeDelete
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
CREATE Procedure dbo.NotificationEventTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationEventType'
)
AS
BEGIN

	IF @KeyType = 'NotificationEventTypeId'
	BEGIN

		EXEC	dbo.NotificationEventTypeDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'NotificationEventTypeId',
				@AuditId	=	@AuditId

		DELETE	 dbo.NotificationEventType
		WHERE	 NotificationEventTypeId = @KeyId


	END

	
	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
