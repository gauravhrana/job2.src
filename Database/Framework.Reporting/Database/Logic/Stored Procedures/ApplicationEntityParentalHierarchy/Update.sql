IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationEntityParentalHierarchyUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchyUpdate'
	DROP  Procedure  ApplicationEntityParentalHierarchyUpdate
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationEntityParentalHierarchyUpdate
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

CREATE Procedure dbo.ApplicationEntityParentalHierarchyUpdate
(
		@ApplicationEntityParentalHierarchyId	INT		
	,	@Name									VARCHAR(50)			
	,	@Description							VARCHAR(50)		
	,	@SortOrder								INT				
	,	@AuditId								INT				
	,	@AuditDate								DATETIME = NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'ApplicationEntityParentalHierarchy'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationEntityParentalHierarchy 
	SET		Name			=	@Name		
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	ApplicationEntityParentalHierarchyId			=	@ApplicationEntityParentalHierarchyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationEntityParentalHierarchyId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO