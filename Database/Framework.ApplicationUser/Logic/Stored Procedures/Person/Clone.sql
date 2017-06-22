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
		@PersonId			INT			= NULL 	OUTPUT		
	,	@FirstName			VARCHAR(50)						
	,	@LastName			VARCHAR(50)						
	,	@MiddleName			VARCHAR(50)						
	,	@PersonTitleId		INT								
	,	@AuditId			INT									
	,	@AuditDate			DATETIME	= NULL				
	,	@SystemEntityType	VARCHAR(50)	= 'Person'			
)
AS
BEGIN
	
	-- This is not applicable for Person table but it wil be applicable for other tables
	--SELECT	@FirstName		= FirstName
	--	,	@LastName		= LastName					
	--FROM	Person
	--WHERE	PersonId = @PersonId

	--EXEC dbo.PersonInsert 
	--		@PersonId	=	NULL
	--	,	@FirstName	=	@FirstName
	--	,	@LastName	=	@LastName
	--	,	@MiddleName =   @MiddleName
	--	,   @PersonTitleId = @PersonTitleId
	--	,	@AuditId	=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
