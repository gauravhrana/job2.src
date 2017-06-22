IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageContextDelete')
BEGIN
	PRINT 'Dropping Procedure HelpPageContextDelete'
	DROP  Procedure HelpPageContextDelete
END
GO

PRINT 'Creating Procedure HelpPageContextDelete'
GO
/******************************************************************************
**		File: 
**		Name: HelpPageContextDelete
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
CREATE Procedure dbo.HelpPageContextDelete
(
		@HelpPageContextId 	INT						
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'HelpPageContext'
)
AS
BEGIN

	DELETE	 dbo.HelpPageContext
	WHERE	 HelpPageContextId = @HelpPageContextId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageContextId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

