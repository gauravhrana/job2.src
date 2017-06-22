IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusDelete')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusDelete'
	DROP  Procedure BatchFileStatusDelete
END
GO

PRINT 'Creating Procedure BatchFileStatusDelete'
GO
/******************************************************************************
**		File: 
**		Name: BatchFileStatusDelete
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
CREATE Procedure dbo.BatchFileStatusDelete
(
		@BatchFileStatusId 		INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileStatus'
)
AS
BEGIN

	DELETE	 dbo.BatchFileStatus
	WHERE	 BatchFileStatusId = @BatchFileStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileStatusId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
