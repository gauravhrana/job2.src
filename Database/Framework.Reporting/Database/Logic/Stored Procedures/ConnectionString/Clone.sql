IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ConnectionStringClone')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringClone'
	DROP  Procedure ConnectionStringClone
END
GO

PRINT 'Creating Procedure ConnectionStringClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ConnectionStringClone
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

CREATE Procedure dbo.ConnectionStringClone
(
		@ConnectionStringId		INT			= NULL 	OUTPUT		
	,	@Name					VARCHAR(100)				
	,	@Description			VARCHAR(100)	
	,	@DataSource				VARCHAR(100)				
	,	@InitialCatalog			VARCHAR(100)		
	,	@UserName				VARCHAR(100)
	,	@Password				VARCHAR(100)
	,	@ProviderName			VARCHAR(100)							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		INT			= 'ConnectionString'				
)
AS
BEGIN		
	
	SELECT	@Name				=	Name			
		,	@Description		=	Description	
		,	@DataSource			=	DataSource		
		,	@InitialCatalog		=	InitialCatalog	
		,	@UserName			=	UserName		
		,	@Password			=	Password		
		,	@ProviderName		=	ProviderName							
	FROM	dbo.ConnectionString 
	WHERE	ConnectionStringId	= @ConnectionStringId
	ORDER BY ConnectionStringId

	EXEC dbo.ConnectionStringInsert 
			@ConnectionStringId	=	NULL
		,	@Name				=	@Name			
		,	@Description		=	@Description	
		,	@DataSource			=	@DataSource		
		,	@InitialCatalog		=	@InitialCatalog	
		,	@UserName			=	@UserName		
		,	@Password			=	@Password		
		,	@ProviderName		=	@ProviderName

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ConnectionStringId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
