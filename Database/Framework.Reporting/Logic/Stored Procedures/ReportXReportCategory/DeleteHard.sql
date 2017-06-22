IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportXReportCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReportXReportCategoryDeleteHard'
	DROP  Procedure ReportXReportCategoryDeleteHard
END
GO

PRINT 'Creating Procedure ReportXReportCategoryDeleteHard'
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
CREATE Procedure dbo.ReportXReportCategoryDeleteHard
(
		@ReportXReportCategoryId		INT				= NULL		
	,	@KeyId 							INT							
	,	@KeyType						VARCHAR(50)					
	,	@AuditId						INT						
	,	@AuditDate						DATETIME		= NULL	
	,	@SystemEntityType				VARCHAR(50)		= 'ReportXReportCategory'
)
AS
BEGIN

	IF @KeyType = 'ReportXReportCategoryId'
	BEGIN

		DELETE	 dbo.ReportXReportCategory
		WHERE	 ReportXReportCategoryId = @KeyId

	END
	ELSE IF @KeyType = 'ReportCategoryId'
	BEGIN

		DELETE	 dbo.ReportXReportCategory
		WHERE	 ReportCategoryId = @KeyId

	END
	ELSE IF @KeyType = 'ReportId'
	BEGIN

		DELETE	 dbo.ReportXReportCategory
			WHERE	 ReportId = @KeyId

	END
	
END
GO
