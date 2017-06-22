IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeDelete'
	DROP  Procedure ApplicationModeDelete
END
GO

PRINT 'Creating Procedure ApplicationModeDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationModeDelete
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
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ApplicationModeDelete
(
		@ApplicationModeId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationMode'
)
AS
BEGIN

	DELETE	 dbo.ApplicationMode
	WHERE	 ApplicationModeId = @ApplicationModeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationMode'
		,	@EntityKey				= @ApplicationModeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
