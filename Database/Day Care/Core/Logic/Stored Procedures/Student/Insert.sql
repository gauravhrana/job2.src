IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentInsert')
BEGIN
	PRINT 'Dropping Procedure StudentInsert'
	DROP  Procedure StudentInsert
END
GO

PRINT 'Creating ProcedureStudentInsert'
GO

/******************************************************************************
**		File: 
**		Name: StudentInsert
**		Desc: 

		This template can be customized:
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.StudentInsert
(
		@StudentId		    INT				= NULL 	OUTPUT
	,	@ApplicationId		INT		 
	,	@LastName			VARCHAR(50)
	,	@FirstName		    VARCHAR(50)
	,   @AuditId		    INT		
    ,   @AuditDate		    DATETIME		= NULL
	,   @SystemEntityType	VARCHAR(50)		= 'Student'		
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @StudentId OUTPUT, @AuditId


	INSERT INTO dbo.Student
	(
			 StudentId
		,	 ApplicationId
		,	 LastName
		,	 FirstName
	)
	VALUES
	(
			@StudentId
		,	@ApplicationId
		,	@LastName
		,	@FirstName
	)


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @StudentId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 

