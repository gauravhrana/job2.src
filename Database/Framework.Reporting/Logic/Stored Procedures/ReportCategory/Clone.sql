IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportCategoryClone')
BEGIN
	PRINT 'Dropping Procedure ReportCategoryClone'
	DROP  Procedure ReportCategoryClone
END
GO

PRINT 'Creating Procedure ReportCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReportCategoryClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/
CREATE Procedure dbo.ReportCategoryClone
(
			@ReportCategoryId		INT				= NULL 	OUTPUT		
		,	@Name					VARCHAR(50)
		,	@Description            VARCHAR(500)						
		,	@SortOrder				INT	
		,	@CreatedDate			DATETIME	= NULL
		,	@ModifiedDate			DATETIME	= NULL
		,	@CreatedByAuditId		INT			= NULL
		,	@ModifiedByAuditId		INT			= NULL				
		,	@AuditId				INT									
		,	@AuditDate				DATETIME		= NULL			
		,	@SystemEntityType		VARCHAR(50)		= 'ReportCategory'
)
AS
BEGIN
		IF @ReportCategoryId IS NULL OR @ReportCategoryId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'ReportCategory', @ReportCategoryId OUTPUT
		END			
	
		SELECT	@Description		= Description
			,	@SortOrder			= SortOrder
			,	@CreatedDate		= CreatedDate
			,	@ModifiedDate		= ModifiedDate
			,	@CreatedByAuditId	= CreatedByAuditId	
			,	@ModifiedByAuditId	= ModifiedByAuditId
								
		FROM	dbo.ReportCategory
		WHERE	ReportCategoryId = @ReportCategoryId

	EXEC dbo.ReportCategoryInsert 
			@ReportCategoryId		=	NULL
		,	@Name			=	@Name
		,	@Description	=	@Description
		,	@SortOrder		=	@SortOrder
		,	@CreatedDate		=	@CreatedDate
		,	@ModifiedDate		=	@ModifiedDate
		,	@CreatedByAuditId	=	@CreatedByAuditId
		,	@ModifiedByAuditId	=	@ModifiedByAuditId
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReportCategory'
		,	@EntityKey				= @ReportCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
