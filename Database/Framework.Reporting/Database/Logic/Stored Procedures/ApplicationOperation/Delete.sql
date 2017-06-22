IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationDelete'
	DROP  Procedure ApplicationOperationDelete
END
GO

PRINT 'Creating Procedure ApplicationOperationDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationOperationDelete
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
CREATE Procedure dbo.ApplicationOperationDelete
(
		@ApplicationOperationId 	INT		
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationOperation'
)
AS
BEGIN

	DELETE	 dbo.ApplicationOperation
	WHERE	 ApplicationOperationId = @ApplicationOperationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
