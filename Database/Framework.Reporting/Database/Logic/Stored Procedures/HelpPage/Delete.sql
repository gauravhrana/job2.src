IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageDelete')
BEGIN
	PRINT 'Dropping Procedure HelpPageDelete'
	DROP  Procedure HelpPageDelete
END
GO

PRINT 'Creating Procedure HelpPageDelete'
GO
/******************************************************************************
**		File: 
**		Name: HelpPageDelete
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
CREATE Procedure dbo.HelpPageDelete
(
		@HelpPageId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'HelpPage'
)
AS
BEGIN

	DELETE	 dbo.HelpPage
	WHERE	 HelpPageId = @HelpPageId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
