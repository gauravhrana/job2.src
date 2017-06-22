IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentDeleteHard')
BEGIN
	PRINT 'Dropping Procedure StudentDeleteHard'
	DROP  Procedure StudentDeleteHard
END
GO

PRINT 'Creating Procedure StudentDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: StudentDeleteHard
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
**     ----------						-----------
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
CREATE Procedure dbo.StudentDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Student'	
)
AS
BEGIN

	IF @KeyType = 'StudentId'
	BEGIN

		EXEC	dbo.AccidentReportDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.ActivityDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.BathroomDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.CommentDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.MealDeleteHard 
				@KeyId		=	@KeyId
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.NeedsDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.SickReportDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.SleepDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.TuitionDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'StudentId'
			,	@AuditId 	=	@AuditId		

		DELETE	 dbo.Student
		WHERE	 StudentId = @KeyId

	END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Student'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
