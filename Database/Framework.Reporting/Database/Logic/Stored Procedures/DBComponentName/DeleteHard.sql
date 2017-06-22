IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBComponentNameDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DBComponentNameDeleteHard'
	DROP  Procedure DBComponentNameDeleteHard
END
GO

PRINT 'Creating Procedure DBComponentNameDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: DBComponentNameDelete
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
CREATE Procedure dbo.DBComponentNameDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'DBComponentName'
)
AS
BEGIN
	IF @KeyType = 'DBComponentNameId'
	BEGIN

		DELETE	 dbo.DBComponentName
		WHERE	 DBComponentNameId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
