IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ModuleDeleteHard'
	DROP  Procedure ModuleDeleteHard
END
GO

PRINT 'Creating Procedure ModuleDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ModuleDelete
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
CREATE Procedure dbo.ModuleDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'Module'
)
AS
BEGIN
	IF @KeyType = 'ModuleId'
	BEGIN

		DELETE	 dbo.Module
		WHERE	 ModuleId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
