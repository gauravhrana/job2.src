IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyUpdate')
BEGIN
	PRINT 'Dropping Procedure SuperKeyUpdate'
	DROP  Procedure  SuperKeyUpdate
END
GO

PRINT 'Creating Procedure SuperKeyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SuperKeyUpdate
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SuperKeyUpdate
(
		@SuperKeyId					INT
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT			
	,	@SystemEntityTypeId			INT			
	,	@ExpirationDate				DATETIME					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKey'
)
AS
BEGIN

	UPDATE	dbo.SuperKey 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder
		,	SystemEntityTypeId		=	@SystemEntityTypeId
		,	ExpirationDate			=	@ExpirationDate							
	WHERE	SuperKeyId				=	@SuperKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKey'
		,	@EntityKey				= @SuperKeyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

 END		
 GO