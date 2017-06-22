IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonClone')
BEGIN
	PRINT 'Dropping Procedure PersonClone'
	DROP  Procedure PersonClone
END
GO

PRINT 'Creating Procedure PersonClone'
GO

/*********************************************************************************************
**		File: 
**		Name: PersonClone
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

CREATE Procedure dbo.PersonClone
(
		@PersonId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			
	,	@LastName				VARCHAR(50)	
	,	@FirstName				VARCHAR(50)						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Person'				
)
AS
BEGIN

	IF @PersonId IS NULL OR @PersonId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Person', @PersonId OUTPUT
	END		

	
	-- This is not applicable for Person table but it wil be applicable for other tables
	--SELECT @LastName		= LastName	
	--	,	 @FirstName		= FirstName	
	--FROM	Person
	--WHERE	PersonId = @PersonId

	EXEC dbo.PersonInsert 
			@PersonId		=	@PersonId
		,	@ApplicationId	=	@ApplicationId
		,	@LastName		=	@LastName
		,	@FirstName		=	@FirstName
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Person'
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByStudentId		= @AuditId	

END	
GO
