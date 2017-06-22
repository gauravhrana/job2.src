IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherList')
BEGIN
	PRINT 'Dropping Procedure TeacherList'
	DROP PROCEDURE TeacherList
END
GO

PRINT 'Creating Procedure TeacherList'
GO

/******************************************************************************
**		File: 
**		Name: TeacherList
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

CREATE Procedure dbo.TeacherList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Teacher'
)
AS
BEGIN
		SELECT	TeacherId	
			,	ApplicationId   
			,	FirstName		  	
			,	LastName
		FROM	dbo.Teacher 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY TeacherId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
