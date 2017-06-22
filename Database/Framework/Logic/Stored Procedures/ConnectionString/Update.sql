IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'ConnectionStringUpdate')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringUpdate'
	DROP  Procedure  ConnectionStringUpdate
END
GO

PRINT 'Creating Procedure ConnectionStringUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ConnectionStringUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ConnectionStringUpdate
(
		@ConnectionStringId			INT		 		
	,	@Name						VARCHAR(100)				
	,	@Description				VARCHAR(100)	
	,	@DataSource					VARCHAR(100)				
	,	@InitialCatalog				VARCHAR(100)		
	,	@UserName					VARCHAR(100)
	,	@Password					VARCHAR(100)
	,	@ProviderName				VARCHAR(100)					
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL						
	,	@SystemEntityType			VARCHAR(50)		= 'ConnectionString'
)
AS
BEGIN

	UPDATE	dbo.ConnectionString 
	SET		Name				=	@Name			
		,	Description			=	@Description	
		,	DataSource			=	@DataSource		
		,	InitialCatalog		=	@InitialCatalog	
		,	UserName			=	@UserName		
		,	Password			=	@Password		
		,	ProviderName		=	@ProviderName	
	WHERE	ConnectionStringId	=	@ConnectionStringId	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO