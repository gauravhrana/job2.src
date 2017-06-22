IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeCategoryDetails')
BEGIN
  PRINT 'Dropping Procedure ThemeCategoryDetails'
  DROP  Procedure ThemeCategoryDetails
END

GO

PRINT 'Creating Procedure ThemeCategoryDetails'
GO


/******************************************************************************
**		File: 
**		Name: ThemeCategoryDetails
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

CREATE Procedure dbo.ThemeCategoryDetails
(
		@ThemeCategoryId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ThemeCategory'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ThemeCategoryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	ThemeCategoryId			
		,	ApplicationId
		,	Name						
		,	Description			
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.ThemeCategory 
	WHERE	ThemeCategoryId = @ThemeCategoryId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeCategory'
		,	@EntityKey				= @ThemeCategoryId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   