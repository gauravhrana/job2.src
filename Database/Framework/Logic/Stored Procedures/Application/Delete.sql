IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationDelete'
	DROP  Procedure ApplicationDelete
END
GO

PRINT 'Creating Procedure ApplicationDelete'
GO
/******************************************************************************
**		Task: 
**		Name: ApplicationDelete
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
CREATE Procedure dbo.ApplicationDelete
(
		@ApplicationId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Application'
)
AS
BEGIN

	DELETE	 dbo.Application
	WHERE	 ApplicationId = @ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
