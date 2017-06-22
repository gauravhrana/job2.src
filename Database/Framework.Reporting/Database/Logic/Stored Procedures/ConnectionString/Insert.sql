IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'ConnectionStringInsert')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringInsert'
	DROP  Procedure ConnectionStringInsert
END
GO

PRINT 'Creating Procedure ConnectionStringInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ConnectionStringInsert
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

CREATE Procedure dbo.ConnectionStringInsert
(
		@ConnectionStringId		INT				= NULL 	OUTPUT		
	,	@Name					VARCHAR(100)				
	,	@Description			VARCHAR(100)	
	,	@DataSource				VARCHAR(100)				
	,	@InitialCatalog			VARCHAR(100)		
	,	@UserName				VARCHAR(100)
	,	@Password				VARCHAR(100)
	,	@ProviderName			VARCHAR(100)
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL						
	,	@SystemEntityType		VARCHAR(50)		= 'ConnectionString'										
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'ConnectionString', @ConnectionStringId OUTPUT
	
	INSERT INTO dbo.ConnectionString 
	( 
			ConnectionStringId						
		,	Name				
		,	Description	
		,	DataSource	
		,	InitialCatalog
		,	UserName
		,	Password
		,	ProviderName							
	)
	VALUES 
	(  
			@ConnectionStringId						
		,	@Name				
		,	@Description	
		,	@DataSource	
		,	@InitialCatalog
		,	@UserName
		,	@Password
		,	@ProviderName		
	)

	SELECT @ConnectionStringId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO

 