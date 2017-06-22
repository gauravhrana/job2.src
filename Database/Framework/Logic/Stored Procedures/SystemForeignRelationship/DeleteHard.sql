IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDeleteHard'
	DROP  Procedure SystemForeignRelationshipDeleteHard
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SystemForeignRelationshipDelete
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
CREATE Procedure dbo.SystemForeignRelationshipDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SystemForeignRelationship'
)
AS
BEGIN
	IF @KeyType = 'SystemForeignRelationshipId'
	BEGIN

		DELETE	 dbo.SystemForeignRelationship
		WHERE	 SystemForeignRelationshipId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
