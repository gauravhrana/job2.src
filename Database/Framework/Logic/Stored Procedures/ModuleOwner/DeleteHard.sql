IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleOwnerDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ModuleOwnerDeleteHard'
	DROP  Procedure ModuleOwnerDeleteHard
END
GO

PRINT 'Creating Procedure ModuleOwnerDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ModuleOwnerDelete
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
CREATE Procedure dbo.ModuleOwnerDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ModuleOwner'
)
AS
BEGIN

	IF @KeyType = 'ModuleOwnerId'
	BEGIN

		DELETE	 dbo.ModuleOwner
		WHERE	 ModuleOwnerId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
