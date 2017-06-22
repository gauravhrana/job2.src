IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteQualitativeDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteQualitativeDelete'
	DROP  Procedure ReleaseNoteQualitativeDelete
END
GO

PRINT 'Creating Procedure ReleaseNoteQualitativeDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteQualitativeDelete
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
CREATE Procedure dbo.ReleaseNoteQualitativeDelete
(
		@ReleaseNoteQualitativeId	INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseNoteQualitative'
)
AS
BEGIN

	DELETE	 dbo.ReleaseNoteQualitative
	WHERE	 ReleaseNoteQualitativeId = @ReleaseNoteQualitativeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteQualitative'
		,	@EntityKey				= @ReleaseNoteQualitativeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
