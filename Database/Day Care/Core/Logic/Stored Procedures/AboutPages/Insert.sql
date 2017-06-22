IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AboutPagesInsert')
BEGIN
	PRINT 'Dropping Procedure AboutPagesInsert'
	DROP PROCEDURE AboutPagesInsert
END
GO

PRINT 'Creating Procedure AboutPagesInsert'
GO

/******************************************************************************
**		File: 
**		Name: AboutPagesInsert
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE dbo.AboutPagesInsert
(
		@AboutPagesId			INT				= NULL 	OUTPUT	
	,	@ApplicationId			INT	
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

	IF @AboutPagesId IS NULL OR @AboutPagesId = -9999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @AboutPagesId OUTPUT
		END
		
	INSERT INTO dbo.AboutPages
	(
			AboutPagesId
		,	ApplicationId	
		,	Description		
		,	Developer
		,	JIRAId
		,	Feature
		,	PrimaryEntity
	)
	VALUES
	(
			@AboutPagesId
		,	@ApplicationId
		,	@Description
		,	@Developer
		,	@JIRAId
		,	@Feature
		,	@PrimaryEntity
	)
	--Create Audit Record
	
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AboutPagesId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

	
END
GO
