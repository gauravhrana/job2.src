IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AboutPagesList')
BEGIN
	PRINT 'Dropping Procedure AboutPagesList'
	DROP PROCEDURE AboutPagesList
END
GO

PRINT 'Creating Procedure AboutPagesList'
GO

/******************************************************************************
**		File: 
**		Name: AboutPagesList
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
CREATE Procedure dbo.AboutPagesList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'AboutPages'
)
AS
BEGIN
		SELECT	AboutPagesId	
			,	ApplicationId   
			,	Description		
			,	Developer
			,	JIRAId
			,	Feature
			,	PrimaryEntity
		FROM   dbo.AboutPages 
		WHERE  ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY AboutPagesId	ASC
			,	 JIRAId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
