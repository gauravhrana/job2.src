IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SickReportClone')
BEGIN
	PRINT 'Dropping Procedure SickReportClone'
	DROP  Procedure SickReportClone
END
GO

PRINT 'Creating Procedure SickReportClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SickReportClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.SickReportClone
(
		@SickReportId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT						
	,	@StudentId				INT								
	,	@TypeOfSickness			VARCHAR(50)						
	,	@AmountOfSickness		VARCHAR(50)						
	,	@FreqOfSickness			VARCHAR(50)						
	,	@TeacherSickNote		VARCHAR(100)					
	,	@ReturnToSchoolDate		DATETIME						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SickReport'				
)

AS

BEGIN

	IF @SickReportId IS NULL OR @SickReportId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'SickReport', @SickReportId OUTPUT
	END	
		
	
	SELECT	@StudentId			=	StudentId
		,	@ApplicationId		=	ApplicationId
		,	@TypeOfSickness		=	TypeOfSickness
		,	@AmountOfSickness	=	AmountOfSickness
		,	@FreqOfSickness		=	FreqOfSickness
		,   @TeacherSickNote	=	TeacherSickNote
		,   @ReturnToSchoolDate	=	ReturnToSchoolDate				
	FROM	dbo.SickReport
	WHERE	SickReportId		= @SickReportId 
	AND		ApplicationId		= @ApplicationId

	EXEC dbo.SickReportInsert 
			@SickReportId		=	NULL
		,	@StudentId			=	@StudentId
		,	@ApplicationId		=	@ApplicationId
		,	@TypeOfSickness		=	@TypeOfSickness
		,	@AmountOfSickness	=	@AmountOfSickness
		,	@FreqOfSickness		=	@FreqOfSickness
		,   @TeacherSickNote	=	@TeacherSickNote
		,   @ReturnToSchoolDate	=	@ReturnToSchoolDate
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'SickReport'	
		,	@EntityKey				= @SickReportId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
