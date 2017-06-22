IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='AboutPagesDoesExist')
BEGIN
	PRINT 'Dropping Procedure AboutPagesDoesExist'
	DROP  Procedure  AboutPagesDoesExist
END
GO

PRINT 'Creating Procedure AboutPagesDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: AboutPagesDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.AboutPagesDoesExist
(
		@JIRAId					VARCHAR(50)		= NULL	
	,	@ApplicationId			INT			
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AboutPages'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.AboutPages a
	WHERE		a.JIRAId = @JIRAId	
	AND			a.ApplicationId	= @ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

