IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentReportDelete')
BEGIN
	PRINT 'Dropping Procedure AccidentReportDelete'
	DROP  Procedure  AccidentReportDelete
END
GO

PRINT 'Creating Procedure AccidentReportDelete'
GO

/******************************************************************************
**		File: 
**		Name: AccidentReportDelete
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

CREATE Procedure dbo.AccidentReportDelete
(
		@AccidentReportId	INT
	,	@AuditId			INT			  
	,	@AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'AccidentReport'	
)
AS
BEGIN
	DELETE	dbo.AccidentReport
	WHERE	AccidentReportId = @AccidentReportId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @AccidentReportId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

