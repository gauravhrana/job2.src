IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryDetails')
BEGIN
  PRINT 'Dropping Procedure MenuCategoryDetails'
  DROP PROCEDURE MenuCategoryDetails
END
GO

PRINT 'Creating Procedure MenuCategoryDetails'
GO

/******************************************************************************
**		File: 
**		Name: MenuCategoryDetails
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

CREATE PROCEDURE dbo.MenuCategoryDetails
(
		@MenuCategoryId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'MenuCategory'
	,	@AddAuditInfo				INT				 = 0
	,	@AddLogggingInfo			INT				 = 0
)
WITH RECOMPILE
AS
BEGIN

	SET NOCOUNT ON

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)	= 'UNKNOWN'
	DECLARE @LastUpdatedDate	AS	DATETIME		= '01/01/1990'
	DECLARE @LastAuditAction	AS	VARCHAR(50)		= 'UNKNOWN'

	IF @AddAuditInfo = 1
	BEGIN
	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@MenuCategoryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT
	END

	SELECT	MenuCategoryId			
		,	ApplicationId
		,	Name						
		,	Description			
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.MenuCategory 
	WHERE	MenuCategoryId = @MenuCategoryId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'MenuCategory'
		,	@EntityKey				= @MenuCategoryId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   