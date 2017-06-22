IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageContextChildrenGet')
BEGIN
	PRINT 'Dropping Procedure HelpPageContextChildrenGet'
	DROP  Procedure HelpPageContextChildrenGet
END
GO

PRINT 'Creating Procedure HelpPageContextChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: HelpPageContextChildrenGet
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

CREATE Procedure dbo.HelpPageContextChildrenGet
(
		@HelpPageContextId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'HelpPageContext'
)
AS
BEGIN

	-- GET HelpPage Records
	SELECT	a.HelpPageId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Content			
		,	a.SortOrder	
		,	a.SystemEntityTypeId	
		,	a.HelpPageContextId
		,	b.EntityName			AS	'SystemEntityType'
		,	c.Name					AS	'HelpPageContext'
	FROM		dbo.HelpPage						a
	INNER JOIN	Configuration.dbo.SystemEntityType	b	ON	a.SystemEntityTypeId	=	b.SystemEntityTypeId
	INNER JOIN	dbo.HelpPageContext					c	ON	a.HelpPageContextId		=	c.HelpPageContextId
	WHERE	a.HelpPageContextId = @HelpPageContextId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageContextId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   