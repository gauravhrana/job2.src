IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDeleteHard'
	DROP  Procedure SuperKeyDeleteHard
END
GO

PRINT 'Creating Procedure SuperKeyDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SuperKeyDelete
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
CREATE Procedure dbo.SuperKeyDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKey'
)
AS
BEGIN

	IF @KeyType = 'SuperKeyId'
	BEGIN

		DELETE	 dbo.SuperKey
		WHERE	 SuperKeyId = @KeyId
	END

	
END
GO
