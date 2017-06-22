IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityCategoryDetails')
BEGIN
  PRINT 'Dropping Procedure SystemEntityCategoryDetails'
  DROP  Procedure SystemEntityCategoryDetails
END

GO

PRINT 'Creating Procedure SystemEntityCategoryDetails'
GO


/******************************************************************************
**		File: 
**		Name: SystemEntityCategoryDetails
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SystemEntityCategoryDetails
(
		@SystemEntityCategoryId		INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SystemEntityCategory'
)
AS
BEGIN
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SystemEntityCategoryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT


	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SystemEntityCategoryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	SystemEntityCategoryId			
		,	ApplicationId
		,	Name						
		,	Description			
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.SystemEntityCategory 
	WHERE	SystemEntityCategoryId = @SystemEntityCategoryId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityCategory'
		,	@EntityKey				= @SystemEntityCategoryId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   