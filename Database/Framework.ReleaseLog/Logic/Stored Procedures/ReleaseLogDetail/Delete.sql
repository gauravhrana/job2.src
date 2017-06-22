IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailDelete'
	DROP  Procedure ReleaseLogDetailDelete
END
GO

PRINT 'Creating Procedure ReleaseLogDetailDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseLogDetailDelete
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
CREATE Procedure dbo.ReleaseLogDetailDelete
(
		@ReleaseLogDetailId 	INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLogDetail'
)
AS
BEGIN

	DELETE	 dbo.ReleaseLogDetail
	WHERE	 ReleaseLogDetailId = @ReleaseLogDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogDetailId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
