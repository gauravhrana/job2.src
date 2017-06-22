IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileHistoryDelete')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryDelete'
	DROP  Procedure BatchFileHistoryDelete
END
GO

PRINT 'Creating Procedure BatchFileHistoryDelete'
GO
/******************************************************************************
**		File: 
**		Name: BatchFileHistoryDelete
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
CREATE Procedure dbo.BatchFileHistoryDelete
(
		@BatchFileHistoryId 	INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileHistory'	
)
AS
BEGIN

	DELETE	 dbo.BatchFileHistory
	WHERE	 BatchFileHistoryId = @BatchFileHistoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @BatchFileHistoryId
		,	@AuditAction			= 'Delete'
		,	@UpdatedDate			= @AuditDate
		,	@UpdatedByPersonId		= @AuditId

END
GO
