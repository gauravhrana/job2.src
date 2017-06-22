IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportXReportCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure ReportXReportCategoryUpdate'
	DROP  Procedure  ReportXReportCategoryUpdate
END
GO

PRINT 'Creating Procedure ReportXReportCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReportXReportCategoryUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ReportXReportCategoryUpdate
(
		@ReportXReportCategoryId	INT		 			
	,	@ReportCategoryId			INT					
	,	@ReportId					INT		
	,	@ApplicationId				INT			
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL	
	,	@SystemEntityType			VARCHAR(50)	= 'ReportXReportCategory'
)
AS
BEGIN 

	UPDATE	dbo.ReportXReportCategory 
	SET		ReportCategoryId			=	@ReportCategoryId			
		,	ReportId					=	@ReportId			
		,	ApplicationId				=	@ApplicationId						
	WHERE	ReportXReportCategoryId	=	@ReportXReportCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReportXReportCategory'
		,	@EntityKey				= @ReportXReportCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO
