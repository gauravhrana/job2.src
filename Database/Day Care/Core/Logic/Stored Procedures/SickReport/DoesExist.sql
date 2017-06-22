IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='SickReportDoesExist')
BEGIN
	PRINT 'Dropping Procedure SickReportDoesExist'
	DROP  Procedure  SickReportDoesExist
END
GO

PRINT 'Creating Procedure SickReportDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: SickReportDoesExist
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.SickReportDoesExist
(
		@ApplicationId			INT					
	,	@StudentId				INT				= NULL		
	,	@TypeOfSickness			VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'SickReport'				
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.SickReport a
	WHERE		a.StudentId			=	@StudentId	
	AND			a.TypeOfSickness	=	@TypeOfSickness	
	AND			a.ApplicationId		=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'SickReport'	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

