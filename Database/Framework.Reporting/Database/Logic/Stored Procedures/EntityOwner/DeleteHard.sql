IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EntityOwnerDeleteHard')
BEGIN
	PRINT 'Dropping Procedure EntityOwnerDeleteHard'
	DROP  Procedure EntityOwnerDeleteHard
END
GO

PRINT 'Creating Procedure EntityOwnerDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: EntityOwnerDelete
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
**		Date:		Author:				Developer:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.EntityOwnerDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'EntityOwner'
)
AS
BEGIN

	IF @KeyType = 'EntityOwnerId'
	BEGIN

		DELETE	 dbo.EntityOwner
		WHERE	 EntityOwnerId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
