IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonInsert')
BEGIN
	PRINT 'Dropping Procedure PersonInsert'
	DROP  Procedure PersonInsert
END
GO

PRINT 'Creating Procedure PersonInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:PersonInsert
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

CREATE Procedure dbo.PersonInsert
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

	IF @PersonId IS NULL OR @PersonId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @PersonId OUTPUT, @AuditId
	END
	
	INSERT INTO dbo.Person 
	( 
			PersonId						
		,	FirstName		
		,	LastName		
		,	MiddleName      
		,	PersonTitleId							
	)
	VALUES 
	(  
			@PersonId		
		,	@FirstName		
		,	@LastName		
		,	@MiddleName		
		,	@PersonTitleId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @PersonId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO
