IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportXReportCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure ReportXReportCategoryDelete'
	DROP  Procedure ReportXReportCategoryDelete
END
GO

PRINT 'Creating Procedure ReportXReportCategoryDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReportXReportCategoryDelete
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
CREATE Procedure dbo.ReportXReportCategoryDelete
(
		@ReportXReportCategoryId 	INT			= NULL		
	,	@ReportCategoryId 			INT			= NULL		
	,	@ReportId 					INT			= NULL	
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ReportXReportCategory'
)
AS
BEGIN

	DELETE	dbo.ReportXReportCategory
	WHERE	ReportXReportCategoryId		=	ISNULL(@ReportXReportCategoryId,ReportXReportCategoryId)	
	AND		ReportCategoryId			=	ISNULL(@ReportCategoryId,ReportCategoryId)
	AND		ReportId					=	ISNULL(@ReportId,ReportId)	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReportXReportCategory'
		,	@EntityKey				= @ReportXReportCategoryId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
