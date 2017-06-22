IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationAttributeDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationAttributeDelete'
	DROP  Procedure ApplicationAttributeDelete
END
GO

PRINT 'Creating Procedure ApplicationAttributeDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationAttributeDelete
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
CREATE Procedure dbo.ApplicationAttributeDelete
(
		@ApplicationId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationAttribute'
)
AS
BEGIN

	DELETE	 dbo.ApplicationAttribute
	WHERE	 ApplicationId = @ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationAttribute'
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
