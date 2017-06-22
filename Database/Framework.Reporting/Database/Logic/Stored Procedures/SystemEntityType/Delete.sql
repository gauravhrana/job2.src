IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeDelete')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeDelete'
	DROP  Procedure SystemEntityTypeDelete
END
GO

PRINT 'Creating Procedure SystemEntityTypeDelete'
GO


/******************************************************************************
**		File: 
**		TableName: SystemEntityTypeDelete
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
**		Date:		Author:				TableDescription:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SystemEntityTypeDelete
(
		@SystemEntityTypeId 	INT							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL						
	,	@SystemEntityType		VARCHAR(50)		= 'SystemEntityType'	
)
AS
BEGIN

	DELETE	 dbo.SystemEntityType
	WHERE	 SystemEntityTypeId = @SystemEntityTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemEntityTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
