IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'ConnectionStringDelete')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringDelete'
	DROP  Procedure ConnectionStringDelete
END
GO

PRINT 'Creating Procedure ConnectionStringDelete'
GO


/******************************************************************************
**		File: 
**		TableName: ConnectionStringDelete
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

CREATE Procedure dbo.ConnectionStringDelete
(
		@ConnectionStringId 	INT							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL						
	,	@SystemEntityType		VARCHAR(50)		= 'ConnectionString'	
)
AS
BEGIN

	DELETE	 dbo.ConnectionString
	WHERE	 ConnectionStringId = @ConnectionStringId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
