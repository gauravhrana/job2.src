IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabChildStructureList')
BEGIN
	PRINT 'Dropping Procedure TabChildStructureList'
	DROP  Procedure  dbo.TabChildStructureList
END
GO

PRINT 'Creating Procedure TabChildStructureList'
GO

/******************************************************************************
**		File: 
**		Name: TabChildStructureList
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				EntityName:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TabChildStructureList
(
		@AuditId				INT	
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TabChildStructure'
)
AS
BEGIN

	SELECT	a.TabChildStructureId			
		,	a.ApplicationId
		,	a.Name						
		,	a.EntityName			
		,	a.SortOrder	
		,	a.TabParentStructureId	
		,	a.InnerControlPath
		,	b.Name					AS	'TabParentStructure'
	FROM		dbo.TabChildStructure		a
	INNER JOIN	dbo.TabParentStructure	b	ON	a.TabParentStructureId	=	b.TabParentStructureId
	WHERE	a.ApplicationId	=	@ApplicationId

	ORDER BY TabChildStructureId			ASC
		,	 SortOrder						ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO