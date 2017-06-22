IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationEntityParentalHierarchyDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchyDelete'
	DROP  Procedure ApplicationEntityParentalHierarchyDelete
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchyDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationEntityParentalHierarchyDelete
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
CREATE Procedure dbo.ApplicationEntityParentalHierarchyDelete
(
		@ApplicationEntityParentalHierarchyId 	INT		
	,	@AuditId								INT						
	,	@AuditDate								DATETIME = NULL			
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationEntityParentalHierarchy'
)
AS
BEGIN

	DELETE	 dbo.ApplicationEntityParentalHierarchy
	WHERE	 ApplicationEntityParentalHierarchyId = @ApplicationEntityParentalHierarchyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @ApplicationEntityParentalHierarchyId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
