IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailsDetails')
BEGIN
  PRINT 'Dropping Procedure ThemeDetailsDetails'
  DROP  Procedure ThemeDetailsDetails
END

GO

PRINT 'Creating Procedure ThemeDetailsDetails'
GO


/******************************************************************************
**		File: 
**		Name: ThemeDetailsDetails
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

CREATE Procedure dbo.ThemeDetailsDetails
(
		@ThemeDetailId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ThemeDetails'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ThemeDetailId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	ThemeDetailId			
		,	ApplicationId
		,	ThemeKeyId						
		,	Value			
		,	ThemeId	
		,	ThemeCategoryId	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM	dbo.ThemeDetails 
	WHERE	ThemeDetailId = @ThemeDetailId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeDetails'
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   