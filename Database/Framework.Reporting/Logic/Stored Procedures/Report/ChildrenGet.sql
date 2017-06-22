IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ReportChildrenGet'
	DROP  Procedure ReportChildrenGet
END
GO

PRINT 'Creating Procedure ReportChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ReportChildrenGet
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

CREATE Procedure dbo.ReportChildrenGet
(
		@ReportId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'Report'
)
AS
BEGIN

	-- GET Report Records
	SELECT	a.ReportXReportCategoryId												
		,	a.ReportId																
		,	a.ReportCategoryId
		,	a.ApplicationId	
		,	b.Name					AS 'Report'
		,	c.Name					AS 'ReportCategory'
	FROM		dbo.ReportXReportCategory	a
	INNER JOIN	dbo.Report			b ON a.ReportId					= b.ReportId
	INNER JOIN	dbo.ReportCategory			c ON a.ReportCategoryId				= c.ReportCategoryId
	WHERE	a.ReportId = @ReportId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReportId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   