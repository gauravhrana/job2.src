IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedsDelete')
BEGIN
	PRINT 'Dropping Procedure NeedsDelete'
	DROP  Procedure  NeedsDelete
END
GO

PRINT 'Creating Procedure NeedsDelete'
GO

/******************************************************************************
**		File: 
**		Name: NeedsDelete
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

CREATE Procedure dbo.NeedsDelete
(
	    @NeedsId	        INT	
	,	@ApplicationId		INT		    = NULL
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,   @SystemEntityType	VARCHAR(50)	= 'Needs'	
)
AS
BEGIN
	DELETE	dbo.Needs
	WHERE	NeedsId = @NeedsId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NeedsId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

