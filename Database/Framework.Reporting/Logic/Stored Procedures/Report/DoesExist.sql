IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ReportDoesExist')
BEGIN
	PRINT 'Dropping Procedure ReportDoesExist'
	DROP  Procedure  ReportDoesExist
END
GO

PRINT 'Creating Procedure ReportDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ReportDoesExist
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

Create procedure dbo.ReportDoesExist
(	
		@ReportId				INT				= NULL	
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)	
	,	@Title					VARCHAR(50)
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Report'		
)
AS
BEGIN	

	SELECT	a.*
	FROM	dbo.Report a
	WHERE	a.Name			=	@Name
	AND		ApplicationId	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReportId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

