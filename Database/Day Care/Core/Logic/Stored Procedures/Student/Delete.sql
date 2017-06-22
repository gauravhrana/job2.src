IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentDelete')
BEGIN
	PRINT 'Dropping Procedure StudentDelete'
	DROP  Procedure  StudentDelete
END
GO

PRINT 'Creating Procedure StudentDelete'
GO

/******************************************************************************
**		File: 
**		Name: StudentDelete
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.StudentDelete
(
		@StudentId			INT	
	,	@AuditId			INT		
    ,	@AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Student'	
)
AS
 BEGIN

		DELETE	dbo.Student
		WHERE	StudentId = @StudentId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @StudentId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

