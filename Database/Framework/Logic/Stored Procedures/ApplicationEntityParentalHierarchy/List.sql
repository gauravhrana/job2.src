IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationEntityParentalHierarchyList')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchyList'
	DROP  Procedure  dbo.ApplicationEntityParentalHierarchyList
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchyList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationEntityParentalHierarchyList
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationEntityParentalHierarchyList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationEntityParentalHierarchy'
)
AS
BEGIN

	SELECT	ApplicationEntityParentalHierarchyId
		,   ApplicationId	
		,	Name
		,	Description
		,	SortOrder				
	FROM		dbo.ApplicationEntityParentalHierarchy 
	ORDER BY	SortOrder		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO