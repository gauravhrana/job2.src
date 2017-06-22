IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteLogisticsDifficultyDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteLogisticsDifficultyDelete'
	DROP  Procedure ReleaseNoteLogisticsDifficultyDelete
END
GO

PRINT 'Creating Procedure ReleaseNoteLogisticsDifficultyDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseNoteLogisticsDifficultyDelete
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
CREATE Procedure dbo.ReleaseNoteLogisticsDifficultyDelete
(
		@ReleaseNoteLogisticsDifficultyId	INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseNoteLogisticsDifficulty'
)
AS
BEGIN

	DELETE	 dbo.ReleaseNoteLogisticsDifficulty
	WHERE	 ReleaseNoteLogisticsDifficultyId = @ReleaseNoteLogisticsDifficultyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteLogisticsDifficulty'
		,	@EntityKey				= @ReleaseNoteLogisticsDifficultyId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
