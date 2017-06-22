IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SickReportInsert')
BEGIN
	PRINT 'Dropping SickReportInsert'
	DROP  Procedure SickReportInsert
END
GO

PRINT 'Creating ProcedureSickReportInsert'
GO

/******************************************************************************
**		File: 
**		Name: pSickReportInsert
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
CREATE Procedure dbo.SickReportInsert
(
		@SickReportId       INT 
	,	@ApplicationId		INT		
	,	@StudentId          INT 
	,	@TypeOfSickness     VARCHAR(50)	 
	,	@AmountOfSickness   VARCHAR(50)  
	,	@FreqOfSickness     VARCHAR(50)  
	,	@TeacherSickNote    VARCHAR(100) 
	,	@ReturnToSchoolDate DATETIME 
	,	@AuditId			INT			  
    ,   @AuditDate		    DATETIME			= NULL    
)
AS
BEGIN	
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SickReportId OUTPUT

	INSERT INTO dbo.SickReport
	(
			SickReportId	
		,	ApplicationId
		,   StudentId
		,	TypeOfSickness
		,	AmountOfSickness
		,	FreqOfSickness
		,   TeacherSickNote
		,	ReturnToSchoolDate
	)
	VALUES
	(
			@SickReportId
		,	@ApplicationId
		,	@StudentId
		,	@TypeOfSickness
		,	@AmountOfSickness
		,	@FreqOfSickness
		,	@TeacherSickNote
		,   @ReturnToSchoolDate
	)
	
--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'SickReport' 
		,	@EntityKey				= @SickReportId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO