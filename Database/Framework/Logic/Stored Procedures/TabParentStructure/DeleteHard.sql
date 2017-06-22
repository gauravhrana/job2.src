IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabParentStructureDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TabParentStructureDeleteHard'
	DROP  Procedure TabParentStructureDeleteHard
END
GO

PRINT 'Creating Procedure TabParentStructureDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: TabParentStructureDelete
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
CREATE Procedure dbo.TabParentStructureDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'TabParentStructure'
)
AS
BEGIN

	IF @KeyType = 'TabParentStructureId'
	BEGIN

		DELETE	 dbo.TabParentStructure
		WHERE	 TabParentStructureId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
