IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessValueDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessValueDelete'
	DROP  Procedure ReleaseNoteBusinessValueDelete
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessValueDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteBusinessValueDelete
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ReleaseNoteBusinessValueDelete
(
		@ReleaseNoteBusinessValueId	INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseNoteBusinessValue'
)
AS
BEGIN

	DELETE	 dbo.ReleaseNoteBusinessValue
	WHERE	 ReleaseNoteBusinessValueId = @ReleaseNoteBusinessValueId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteBusinessValue'
		,	@EntityKey				= @ReleaseNoteBusinessValueId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
