IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentList')
BEGIN
	PRINT 'Dropping Procedure StudentList'
	DROP PROCEDURE StudentList
END
GO

PRINT 'Creating Procedure StudentList'
GO

/******************************************************************************
**		File: 
**		Name: StudentList
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

CREATE Procedure dbo.StudentList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Student'
)
AS
BEGIN
		SELECT	StudentId	
			,	ApplicationId   
			,	FirstName		  	
			,	LastName
		FROM	dbo.Student 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY StudentId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
