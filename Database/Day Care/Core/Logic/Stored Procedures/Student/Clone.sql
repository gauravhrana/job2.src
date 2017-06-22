IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentClone')
BEGIN
	PRINT 'Dropping Procedure StudentClone'
	DROP  Procedure StudentClone
END
GO

PRINT 'Creating Procedure StudentClone'
GO

/*********************************************************************************************
**		File: 
**		Name: StudentClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.StudentClone
(
		@StudentId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT							
	,	@FirstName				VARCHAR(50)							
	,	@LastName				VARCHAR(50)						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Student'				
)
AS
BEGIN

	IF @StudentId IS NULL OR @StudentId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Student', @StudentId OUTPUT
	END		

	
	-- This is not applicable for Student table but it wil be applicable for other tables
	--SELECT@FirstName		= FirstName
	--	,	@LastName		= LastName					
	--FROM	Student
	--WHERE	StudentId = @StudentId

	EXEC dbo.StudentInsert 
			@StudentId		=	@StudentId
		,	@ApplicationId	=	@ApplicationId
		,	@FirstName		=	@FirstName
		,	@LastName		=	@LastName
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Student'
		,	@EntityKey				= @StudentId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId	

END	
GO
