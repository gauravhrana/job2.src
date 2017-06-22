IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipTypeList')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipTypeList'
	DROP  Procedure  dbo.SystemForeignRelationshipTypeList
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipTypeList'
GO

/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipTypeList
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
CREATE Procedure dbo.SystemForeignRelationshipTypeList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL		
	,	@ApplicationId			INT			= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SystemForeignRelationshipType'
)
AS
BEGIN

	SELECT	SystemForeignRelationshipTypeId	
		,	ApplicationId   
		,	Name		  	
		,	SortOrder
	FROM	dbo.SystemForeignRelationshipType 
	WHERE	ApplicationId = ISNULL(@ApplicationId, ApplicationId)
	ORDER BY SortOrder	ASC
		 
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO