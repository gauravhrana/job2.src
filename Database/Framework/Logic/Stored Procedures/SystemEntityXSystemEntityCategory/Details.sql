IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityXSystemEntityCategoryDetails')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategoryDetails'
	DROP  Procedure SystemEntityXSystemEntityCategoryDetails
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategoryDetails'
GO

/******************************************************************************
**		File: 
**		PersonId: SystemEntityXSystemEntityCategoryDetails
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

CREATE Procedure dbo.SystemEntityXSystemEntityCategoryDetails
(
		@SystemEntityXSystemEntityCategoryId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityTypeId		INT			= 'SystemEntityXSystemEntityCategory'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SystemEntityXSystemEntityCategoryId
		,	@SystemEntityTypeId		=	@SystemEntityTypeId
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		

	SELECT	a.SystemEntityXSystemEntityCategoryId												
		,	a.SystemEntityId																
		,	a.SystemEntityCategoryId
		,	a.ApplicationId	
		,	b.EntityName					AS	'SystemEntity'
		,	c.Name					AS	'SystemEntityCategory'	
		,	@LastUpdatedDate		AS	'UpdatedDate' 
		,	@LastUpdatedBy			AS	'UpdatedBy'   
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.SystemEntityXSystemEntityCategory	a
	INNER JOIN	dbo.SystemEntityType			b ON a.SystemEntityId					= b.SystemEntityTypeId
	INNER JOIN	dbo.SystemEntityCategory			c ON a.SystemEntityCategoryId				= c.SystemEntityCategoryId
	WHERE	SystemEntityXSystemEntityCategoryId = @SystemEntityXSystemEntityCategoryId		

	--Create Audit History
	EXEC dbo.AuditHistoryInsert
			@SystemEntityId			= @SystemEntityTypeId
		,	@EntityKey				= @SystemEntityXSystemEntityCategoryId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
   