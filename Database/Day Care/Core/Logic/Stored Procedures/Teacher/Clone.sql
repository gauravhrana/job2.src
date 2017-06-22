IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherClone')
BEGIN
	PRINT 'Dropping Procedure TeacherClone'
	DROP  Procedure TeacherClone
END
GO

PRINT 'Creating Procedure TeacherClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TeacherClone
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

CREATE Procedure dbo.TeacherClone
(
		@TeacherId				INT			= NULL 	OUTPUT			
	,	@ApplicationId			INT			
	,	@FirstName				VARCHAR(50)						
	,	@LastName				VARCHAR(50)						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Teacher'				
)

AS

BEGIN

	IF @TeacherId IS NULL OR @TeacherId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Teacher', @TeacherId OUTPUT
	END		

	
	-- This is not applicable for Teacher table but it wil be applicable for other tables
	--SELECT @FirstName		= FirstName
	--	,	 @LastName		= LastName					
	--FROM	Teacher
	--WHERE	TeacherId = @TeacherId

	EXEC dbo.TeacherInsert 
			@TeacherId		=	@TeacherId
		,	@ApplicationId	=	@ApplicationId
		,	@FirstName		=	@FirstName
		,	@LastName		=	@LastName
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Teacher'
		,	@EntityKey				= @TeacherId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByTeacherId		= @AuditId	

END	
GO
