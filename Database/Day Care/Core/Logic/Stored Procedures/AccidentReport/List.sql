IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentReportList')
BEGIN
	PRINT 'Dropping Procedure AccidentReportList'
	DROP PROCEDURE AccidentReportList
END
GO

PRINT 'Creating Procedure AccidentReportList'
GO

/******************************************************************************
**		File: 
**		Name: AccidentReportList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.AccidentReportList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'AccidentReport'
)
AS
BEGIN
		SELECT	a.AccidentReportId
			,	a.ApplicationId		
			,	a.StudentId				
			,	a.Date		
			,	a.AccidentPlaceId
			,	a.TeacherId
			,   a.Description
			,	a.Remedy
			,	a.SignoffParent
			,	a.SignoffTeacher
			,	a.SignoffAdmin
		FROM   dbo.AccidentReport  a
		WHERE  ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY AccidentReportId	ASC
		
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
