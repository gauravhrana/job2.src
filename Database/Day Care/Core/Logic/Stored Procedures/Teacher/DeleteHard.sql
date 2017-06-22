IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TeacherDeleteHard'
	DROP  Procedure TeacherDeleteHard
END
GO

PRINT 'Creating Procedure TeacherDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: TeacherDeleteHard
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
CREATE Procedure dbo.TeacherDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Teacher'
)
AS
BEGIN

	IF @KeyType = 'TeacherId'
	BEGIN

		EXEC	dbo.AccidentReportDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'TeacherId'
			,	@AuditId 	=	@AuditId

		EXEC	dbo.BathroomDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'TeacherId'
			,	@AuditId 	=	@AuditId		

		DELETE	 dbo.Teacher
		WHERE	 TeacherId = @KeyId

	END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Teacher'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
