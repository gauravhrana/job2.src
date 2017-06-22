IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ApplicationChildrenGet'
	DROP  Procedure ApplicationChildrenGet
END
GO

PRINT 'Creating Procedure ApplicationChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationChildrenGet
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

CREATE Procedure dbo.ApplicationChildrenGet
(
		@ApplicationId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'Application'
)
AS
BEGIN

	-- GET ApplicationMode Records
	SELECT	a.*	
	FROM	Configuration.dbo.ApplicationMode a	
	WHERE	a.ApplicationId = @ApplicationId

	-- GET Project Records
	SELECT	a.*	
	FROM	TaskTimeTracker.dbo.Project a	
	WHERE	a.ApplicationId = @ApplicationId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   