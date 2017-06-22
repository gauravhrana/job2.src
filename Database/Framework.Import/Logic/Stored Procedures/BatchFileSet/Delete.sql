IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetDelete')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetDelete'
	DROP  Procedure BatchFileSetDelete
END
GO

PRINT 'Creating Procedure BatchFileSetDelete'
GO
/******************************************************************************
**		File: 
**		Name: BatchFileSetDelete
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
CREATE Procedure dbo.BatchFileSetDelete
(
		@BatchFileSetId 		INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileSet'
)
AS
BEGIN

	DELETE	 dbo.BatchFileSet
	WHERE	 BatchFileSetId = @BatchFileSetId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileSetId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
