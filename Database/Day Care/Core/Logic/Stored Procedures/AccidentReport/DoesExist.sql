IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='AccidentReportDoesExist')
BEGIN
	PRINT 'Dropping Procedure AccidentReportDoesExist'
	DROP  Procedure  AccidentReportDoesExist
END
GO

PRINT 'Creating Procedure AccidentReportDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: AccidentReportDoesExist
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

Create procedure dbo.AccidentReportDoesExist
(
		@StudentId				INT				= NULL	
	,	@ApplicationId			INT	
	,	@Date					DATETIME		= NULL		
	,	@AccidentPlaceId		INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentReport'			
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.AccidentReport a
	WHERE		a.StudentId			=	@StudentId	
	AND			a.DATE				=	@Date
	AND			a.AccidentPlaceId	=	@AccidentPlaceId
	AND			@ApplicationId		=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

