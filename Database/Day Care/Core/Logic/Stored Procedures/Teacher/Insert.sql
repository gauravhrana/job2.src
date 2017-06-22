IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherInsert')
BEGIN
	PRINT 'Dropping Procedure TeacherInsert'
	DROP  Procedure TeacherInsert
END
GO

PRINT 'Creating Procedure TeacherInsert'
GO

/******************************************************************************
**		File: 
**		Name: TeacherInsert
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
CREATE Procedure dbo.TeacherInsert
(
		@TeacherId		    INT
	,	@ApplicationId		INT				
	,	@LastName			VARCHAR(50)
	,	@FirstName		    VARCHAR(50)
	,   @AuditId            INT		
    ,   @AuditDate          DATETIME	= NULL	
	,   @SystemEntityType	VARCHAR(50)	= 'Teacher'
)
AS
BEGIN	

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TeacherId OUTPUT, @AuditId
	 	
	INSERT INTO dbo.Teacher
	(
			 TeacherId
		,	 ApplicationId
		,	 LastName
		,	 FirstName
	)
	VALUES
	(
			@TeacherId
		,	@ApplicationId
		,	@LastName
		,	@FirstName
	)
--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TeacherId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
