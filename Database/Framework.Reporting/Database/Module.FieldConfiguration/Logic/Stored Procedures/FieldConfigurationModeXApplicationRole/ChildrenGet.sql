IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ClientChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ClientChildrenGet'
	DROP  Procedure ClientChildrenGet
END
GO

PRINT 'Creating Procedure ClientChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ClientChildrenGet
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

CREATE Procedure dbo.ClientChildrenGet
(
		@ClientId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'Client'
)
AS
BEGIN

	-- GET ClientXProject Records
	SELECT	ClientXProjectId
		,	ApplicationId
		,	ClientId
		,	ProjectId
	FROM	dbo.ClientXProject a
	WHERE	a.ClientId = @ClientId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ClientId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   