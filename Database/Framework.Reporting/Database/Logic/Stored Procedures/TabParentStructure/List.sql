IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabParentStructureList')
BEGIN
	PRINT 'Dropping Procedure TabParentStructureList'
	DROP  Procedure  dbo.TabParentStructureList
END
GO

PRINT 'Creating Procedure TabParentStructureList'
GO

/******************************************************************************
**		File: 
**		Name: TabParentStructureList
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
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TabParentStructureList
(
		@AuditId				INT	
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TabParentStructure'
)
AS
BEGIN

	SELECT	a.TabParentStructureId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Description			
		,	a.SortOrder	
		,	a.IsAllTab	
	FROM		dbo.TabParentStructure		a
	WHERE	a.ApplicationId	=	@ApplicationId

	ORDER BY TabParentStructureId			ASC
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