IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AboutPagesDelete')
BEGIN
	PRINT 'Dropping Procedure AboutPagesDelete'
	DROP  Procedure  AboutPagesDelete
END
GO

PRINT 'Creating Procedure AboutPagesDelete'
GO

/******************************************************************************
**		File: 
**		Name: AboutPagesDelete
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

CREATE Procedure dbo.AboutPagesDelete
(
		@AboutPagesId			INT     
	,	@AuditId				INT			  
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'AboutPages'
)
AS
BEGIN

	DELETE	dbo.AboutPages
	WHERE	AboutPagesId = @AboutPagesId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @AboutPagesId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

