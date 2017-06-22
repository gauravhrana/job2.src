IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationInsert'
	DROP  Procedure ApplicationInsert
END
GO

PRINT 'Creating Procedure ApplicationInsert'
GO

/*********************************************************************************************
**		Task: 
**		Name:ApplicationInsert
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
**********************************************************************************************/

CREATE Procedure dbo.ApplicationInsert
(
		@ApplicationId			INT				= NULL 	OUTPUT		
	,	@Name					VARCHAR(50)							
	,	@Description			VARCHAR(50)							
	,	@SortOrder				INT	
	,	@Code					VARCHAR(50)																							
	,	@AuditId				INT										
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'Application'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationId OUTPUT, @AuditId
	
	INSERT INTO dbo.Application 
	( 
			ApplicationId							
		,	Name					
		,	Description				
		,	SortOrder	
		,	Code											
	)
	VALUES 
	(  
			@ApplicationId			
		,	@Name					
		,	@Description			
		,	@SortOrder		
		,	@Code			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@AuditAction			= 'Insert'	 
		,	@EntityKey				= @ApplicationId
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 