IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileDelete')
BEGIN
	PRINT 'Dropping Procedure BatchFileDelete'
	DROP  Procedure BatchFileDelete
END
GO

PRINT 'Creating Procedure BatchFileDelete'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileDelete
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

CREATE Procedure dbo.BatchFileDelete
(
		@BatchFileId 		INT						
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'BatchFile'
)
AS
BEGIN	
	
	DELETE	 dbo.BatchFile
	WHERE	 BatchFileId = @BatchFileId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
