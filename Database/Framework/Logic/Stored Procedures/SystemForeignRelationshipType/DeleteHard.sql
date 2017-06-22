IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipTypeDeleteHard'
	DROP  Procedure SystemForeignRelationshipTypeDeleteHard
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipTypeDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SystemForeignRelationshipTypeDelete
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
CREATE Procedure dbo.SystemForeignRelationshipTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SystemForeignRelationshipType'	
)
AS
BEGIN
		IF @KeyType = 'SystemForeignRelationshipTypeId'
		BEGIN

		EXEC	@KeyId		=	@KeyId 
				@KeyType	=	'SystemForeignRelationshipTypeId'  
			,	@AuditId	=	@AuditId

		DELETE	 dbo.SystemForeignRelationshipType
		WHERE	 SystemForeignRelationshipTypeId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
