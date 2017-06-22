IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDatabaseDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDatabaseDeleteHard'
	DROP  Procedure SystemForeignRelationshipDatabaseDeleteHard
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDatabaseDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SystemForeignRelationshipDatabaseDelete
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
CREATE Procedure dbo.SystemForeignRelationshipDatabaseDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SystemForeignRelationshipDatabase'	
)
AS
BEGIN
		IF @KeyType = 'SystemForeignRelationshipDatabaseId'
		BEGIN

		EXEC	@KeyId		=	@KeyId 
				@KeyType	=	'SystemForeignRelationshipDatabaseId'  
			,	@AuditId	=	@AuditId

		DELETE	 dbo.SystemForeignRelationshipDatabase
		WHERE	 SystemForeignRelationshipDatabaseId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
