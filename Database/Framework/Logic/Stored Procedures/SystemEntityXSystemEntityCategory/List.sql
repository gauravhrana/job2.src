IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityXSystemEntityCategoryList')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategoryList'
	DROP  Procedure  dbo.SystemEntityXSystemEntityCategoryList
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategoryList'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityXSystemEntityCategoryList
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

CREATE Procedure dbo.SystemEntityXSystemEntityCategoryList
(
		@AuditId				INT		
	,	@ApplicationId			INT		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityXSystemEntityCategory'
)
AS
BEGIN

	SELECT	a.SystemEntityXSystemEntityCategoryId												
		,	a.SystemEntityId																
		,	a.SystemEntityCategoryId
		,	a.ApplicationId	
		,	b.EntityName					AS	'SystemEntity'
		,	c.Name					AS 'SystemEntityCategory'
	FROM		dbo.SystemEntityXSystemEntityCategory	a
	INNER JOIN	dbo.SystemEntityType			b ON a.SystemEntityId					= b.SystemEntityTypeId
	INNER JOIN	dbo.SystemEntityCategory			c ON a.SystemEntityCategoryId				= c.SystemEntityCategoryId
	WHERE	a.ApplicationId = @ApplicationId

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO