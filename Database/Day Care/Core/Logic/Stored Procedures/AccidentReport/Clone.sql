IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentReportClone')
BEGIN
	PRINT 'Dropping Procedure AccidentReportClone'
	DROP  Procedure AccidentReportClone
END
GO

PRINT 'Creating Procedure AccidentReportClone'
GO

/*********************************************************************************************
**		File: 
**		Name: AccidentReportClone
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

CREATE Procedure dbo.AccidentReportClone
(
		@AccidentReportId		INT			= NULL 	OUTPUT		
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
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'AccidentReport'				
)

AS
BEGIN

	IF @AccidentReportId IS NULL OR @AccidentReportId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @AccidentReportId OUTPUT
	END			
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@StudentID			=	StudentId	
	    ,	@Date				=	Date
	    ,	@AccidentPlaceId	=	AccidentPlaceId
	    ,	@TeacherId			=	TeacherId 
		,	@Description		=	Description
		,	@Remedy				=	Remedy
		,	@SignoffParent		=	SignoffParent
		,	@SignoffTeacher		=	SignoffTeacher
		,	@SignoffAdmin		=	SignoffAdmin				
	FROM	dbo.AccidentReport
	WHERE	AccidentReportId	= @AccidentReportId 
	AND		ApplicationId		= @ApplicationId

	EXEC dbo.AccidentReportInsert 
			@ApplicationId		=	@ApplicationId
		,	@StudentID			=	@StudentId	
	    ,	@Date				=	@Date
	    ,	@AccidentPlaceId	=	@AccidentPlaceId
	    ,	@AccidentPlace		=	@AccidentPlace
	    ,	@TeacherId			=	@TeacherId 
		,	@Description		=	@Description
		,	@Remedy				=	@Remedy
		,	@SignoffParent		=	@SignoffParent
		,	@SignoffTeacher		=	@SignoffTeacher
		,	@SignoffAdmin		=	@SignoffAdmin
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @AccidentReportId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
