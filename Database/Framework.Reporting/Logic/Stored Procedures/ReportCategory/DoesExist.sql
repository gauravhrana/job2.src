IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ReportCategoryDoesExist')
BEGIN
	PRINT 'Dropping Procedure ReportCategoryDoesExist'
	DROP  Procedure  ReportCategoryDoesExist
END
GO

PRINT 'Creating Procedure ReportCategoryDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ReportCategoryDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.ReportCategoryDoesExist
(	
		@ReportCategoryId		INT				= NULL		
	,	@Name					VARCHAR(50)	
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'ReportCategory'		
)
AS
BEGIN	

	SELECT	a.*
	FROM	dbo.ReportCategory a
	WHERE	a.Name			=	@Name

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReportCategoryId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

