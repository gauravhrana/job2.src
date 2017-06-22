IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationUpdate'
	DROP  Procedure  ApplicationUpdate
END
GO

PRINT 'Creating Procedure ApplicationUpdate'
GO

/******************************************************************************
**		Task: 
**		Name: ApplicationUpdate
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

CREATE Procedure dbo.ApplicationUpdate
(
		@ApplicationId			INT		 				
	,	@Name					VARCHAR(50)					
	,	@Description			VARCHAR(50)				
	,	@SortOrder				INT	
	,	@Code					VARCHAR(50)									
	,	@AuditId				INT							
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Application'
)
AS
BEGIN 

	UPDATE	dbo.Application 
	SET		Name				=	@Name				
		,	Description			=	@Description				
		,	SortOrder			=	@SortOrder		
		,	Code				=	@Code												
	WHERE	ApplicationId		=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@AuditAction			= 'Update' 
		,	@EntityKey				= @ApplicationId
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO