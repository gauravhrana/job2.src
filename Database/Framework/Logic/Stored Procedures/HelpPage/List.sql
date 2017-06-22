IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='HelpPageList')
BEGIN
	PRINT 'Dropping Procedure HelpPageList'
	DROP Procedure HelpPageList
END
GO

PRINT 'Creating Procedure HelpPageList'
GO

/******************************************************************************
**		File: 
**		Name: HelpPageList
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC HelpPageList NULL	, NULL	, NULL
			EXEC HelpPageList NULL	, 'K'	, NULL
			EXEC HelpPageList 1		, 'K'	, NULL
			EXEC HelpPageList 1		, NULL	, NULL
			EXEC HelpPageList NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Content:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure HelpPageList
(
		@ApplicationId				INT				= NULL				
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'HelpPage'
)
AS
BEGIN
	
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
	WHERE	a.ApplicationId			=	@ApplicationId
	ORDER BY a.SortOrder		ASC,
			 a.HelpPageId		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	

END
GO
