IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherDelete')
BEGIN
	PRINT 'Dropping Procedure TeacherDelete'
	DROP  Procedure  TeacherDelete
END
GO

PRINT 'Creating Procedure TeacherDelete'
GO

/******************************************************************************
**		File: 
**		Name: TeacherDelete
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

CREATE Procedure dbo.TeacherDelete
(
		@TeacherId	        INT	
	,	@AuditId            INT		
    ,	@AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Teacher'
)
AS
 BEGIN
	DELETE	dbo.Teacher
	WHERE	TeacherId = @TeacherId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TeacherId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

