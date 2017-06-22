IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportXReportCategoryClone')
BEGIN
	PRINT 'Dropping Procedure ReportXReportCategoryClone'
	DROP  Procedure ReportXReportCategoryClone
END
GO

PRINT 'Creating Procedure ReportXReportCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReportXReportCategoryClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/
CREATE Procedure dbo.ReportXReportCategoryClone
(
		@ReportXReportCategoryId		INT			= NULL 	OUTPUT		
	,	@ReportCategoryId				INT								
	,	@ReportId						INT		
	,	@ApplicationId					INT				
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL				
	,	@SystemEntityType				VARCHAR(50)	= 'ReportXReportCategory'				
)
AS
BEGIN
	IF @ReportXReportCategoryId IS NULL OR @ReportXReportCategoryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'ReportXReportCategory', @ReportXReportCategoryId OUTPUT
	END			
	
	SELECT	@ReportCategoryId			= ReportCategoryId
		,	@ReportId					= ReportId	
		,	@ApplicationId				= ApplicationId		
	FROM	dbo.ReportXReportCategory
	WHERE	ReportXReportCategoryId		= @ReportXReportCategoryId

	EXEC dbo.ReportXReportCategoryInsert 
			@ReportXReportCategoryId		=	NULL
		,	@ReportCategoryId				=	@ReportCategoryId
		,	@ReportId						=	@ReportId		
		,	@ApplicationId					=	@ApplicationId
		,	@AuditId						=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReportXReportCategory'
		,	@EntityKey				= @ReportXReportCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
