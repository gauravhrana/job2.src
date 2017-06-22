IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBNameDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DBNameDeleteHard'
	DROP  Procedure DBNameDeleteHard
END
GO

PRINT 'Creating Procedure DBNameDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: DBNameDelete
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
CREATE Procedure dbo.DBNameDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'DBName'
)
AS
BEGIN
	IF @KeyType = 'DBNameId'
	BEGIN

		DELETE	 dbo.DBName
		WHERE	 DBNameId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
