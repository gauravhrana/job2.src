IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabChildStructureDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TabChildStructureDeleteHard'
	DROP  Procedure TabChildStructureDeleteHard
END
GO

PRINT 'Creating Procedure TabChildStructureDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: TabChildStructureDelete
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
**		Date:		Author:				EntityName:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TabChildStructureDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'TabChildStructure'
)
AS
BEGIN
	IF @KeyType = 'TabChildStructureId'
	BEGIN

		DELETE	 dbo.TabChildStructure
		WHERE	 TabChildStructureId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
