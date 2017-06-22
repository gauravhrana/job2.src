IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDelete'
	DROP  Procedure ReleaseLogDelete
END
GO

PRINT 'Creating Procedure ReleaseLogDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseLogDelete
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
CREATE Procedure dbo.ReleaseLogDelete
(
		@ReleaseLogId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLog'
)
AS
BEGIN

	DELETE	 dbo.ReleaseLog
	WHERE	 ReleaseLogId = @ReleaseLogId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
