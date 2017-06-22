IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentReportInsert')
BEGIN
	PRINT 'Dropping Procedure AccidentReportInsert'
	DROP  Procedure  AccidentReportInsert
END
GO

PRINT 'Creating Procedure AccidentReportInsert'
GO

/******************************************************************************
**		File: 
**		Name: AccidentReportInsert
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
CREATE Procedure dbo.AccidentReportInsert
(
		@AccidentReportID		INT
	,	@ApplicationId			INT
	,	@StudentID				INT
	,	@Date					DATETIME
	,	@AccidentPlaceID		INT
	,	@AccidentPlace			VARCHAR(50)
	,	@TeacherID				INT
	,	@Description			VARCHAR(500)	
	,	@Remedy					VARCHAR(200)
	,	@SignoffParent			BIT
	,	@SignoffTeacher			BIT
	,	@SignoffAdmin			BIT
	,   @AuditId				INT			 
    ,   @AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'AccidentReport'		
)
AS
BEGIN
	
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @AccidentReportId OUTPUT
		
	INSERT INTO dbo.AccidentReport
	(
			AccidentReportId
		,	ApplicationId	
	    ,	StudentId	
	    ,	Date
	    ,	AccidentPlaceId
	    ,	TeacherId 
		,	Description
		,	Remedy
		,	SignoffParent
		,	SignoffTeacher
		,	SignoffAdmin
	)
	VALUES
	(
			@AccidentReportId
		,	@ApplicationId	
	    ,	@StudentId	
	    ,	@Date
	    ,	@AccidentPlaceId
	    ,	@TeacherId 
		,	@Description
		,	@Remedy
		,	@SignoffParent
		,	@SignoffTeacher
		,	@SignoffAdmin
	)

	
--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AccidentReportId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
