IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ConnectionStringXApplicationDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringXApplicationDeleteHard'
	DROP  Procedure ConnectionStringXApplicationDeleteHard
END
GO

PRINT 'Creating Procedure ConnectionStringXApplicationDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ConnectionStringXApplicationDelete
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
CREATE Procedure dbo.ConnectionStringXApplicationDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ConnectionStringXApplication'	 
)
AS
BEGIN

	IF @KeyType = 'ConnectionStringXApplicationId'
		BEGIN

			DELETE	 dbo.ConnectionStringXApplication
			WHERE	 ConnectionStringXApplicationId = @KeyId

		END
	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
