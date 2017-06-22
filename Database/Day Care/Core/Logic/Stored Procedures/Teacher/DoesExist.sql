IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherDoesExist')
BEGIN
	PRINT 'Dropping Procedure TeacherDoesExist'
	DROP  Procedure  TeacherDoesExist
END
GO

PRINT 'Creating Procedure TeacherDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TeacherDoesExist
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TeacherDoesExist
(	
		@ApplicationId			INT					
	,	@LastName				VARCHAR(50)		= NULL		
	,	@FirstName				VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'Teacher'
)
AS
BEGIN	
		
	SELECT		*
	FROM		dbo.Teacher
	WHERE		LastName		= @LastName
	AND			FirstName		= @FirstName
	AND			ApplicationId	= @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Teacher' 
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByTeacherId		= @AuditId
END
GO
