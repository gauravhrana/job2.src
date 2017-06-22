IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AboutPagesUpdate')
BEGIN
	PRINT 'Dropping Procedure AboutPagesUpdate'
	DROP  Procedure  AboutPagesUpdate
END
GO

PRINT 'Creating Procedure AboutPagesUpdate'

GO

/******************************************************************************
**		File: 
**		Name: AboutPagesUpdate
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
**		----------						-----------
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

CREATE Procedure dbo.AboutPagesUpdate
(          
		@AboutPagesId		INT			
	,	@Description			VARCHAR (500) 
	,	@Developer				VARCHAR (100) 
	,	@JIRAId					VARCHAR (100)
	,	@Feature				VARCHAR (100)
	,	@PrimaryEntity			VARCHAR (100)
	,   @AuditId				INT				
	,   @AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'AboutPages'
)
AS
BEGIN
	UPDATE	dbo.AboutPages
	SET		Description			= @Description
		,	Developer			= @Developer
		,	JIRAId				= @JIRAId
		,	Feature				= @Feature
		,	PrimaryEntity		= @PrimaryEntity	
	WHERE	AboutPagesId		= @AboutPagesId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AboutPagesId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

