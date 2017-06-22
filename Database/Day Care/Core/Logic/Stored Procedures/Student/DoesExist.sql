IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentDoesExist')
BEGIN
	PRINT 'Dropping Procedure StudentDoesExist'
	DROP  Procedure  StudentDoesExist
END
GO

PRINT 'Creating Procedure StudentDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: StudentDoesExist
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
CREATE Procedure dbo.StudentDoesExist
(	
		@ApplicationId			INT						
	,	@FirstName				VARCHAR(50)		= NULL		
	,	@LastName				VARCHAR(50)		= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
    ,	@SystemEntityType		VARCHAR(50)		= 'Student'	
)
AS
BEGIN	
		
	SELECT		*
	FROM		dbo.Student
	WHERE		FirstName	= @FirstName
	AND			LastName	= @LastName
	AND			ApplicationId = @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Student'
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId
END
GO
