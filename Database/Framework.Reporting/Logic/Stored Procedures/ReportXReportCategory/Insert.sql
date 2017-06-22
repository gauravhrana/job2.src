IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportXReportCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure ReportXReportCategoryInsert'
	DROP  Procedure ReportXReportCategoryInsert
END
GO

PRINT 'Creating Procedure ReportXReportCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReportXReportCategoryInsert
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
**********************************************************************************************/

CREATE Procedure dbo.ReportXReportCategoryInsert
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
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReportXReportCategoryId OUTPUT, @AuditId
	
	INSERT INTO dbo.ReportXReportCategory 
	( 
			ReportXReportCategoryId						
		,	ReportCategoryId				
		,	ReportId		
		,	ApplicationId	
	)

	VALUES 
	(  
			@ReportXReportCategoryId					
		,	@ReportCategoryId			 
		,	@ReportId	
		,	@ApplicationId			
	)
-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReportXReportCategory'
		,	@EntityKey				= @ReportXReportCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 