IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityCategoryList')
BEGIN
	PRINT 'Dropping Procedure SystemEntityCategoryList'
	DROP  Procedure  dbo.SystemEntityCategoryList
END
GO

PRINT 'Creating Procedure SystemEntityCategoryList'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityCategoryList
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

CREATE Procedure dbo.SystemEntityCategoryList
(
		@ApplicationId			INT			= NULL	
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityCategory'
)
AS
BEGIN

	SELECT	SystemEntityCategoryId	
		,   ApplicationId   
		,	Name		  	
		,	Description	   
		,	SortOrder
	 FROM	dbo.SystemEntityCategory 
	 WHERE ApplicationId = ISNULL(@ApplicationId, ApplicationId)
	ORDER BY SystemEntityCategoryId			ASC
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