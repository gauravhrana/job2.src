IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditActionUpdate')
BEGIN
	PRINT 'Dropping Procedure AuditActionUpdate'
	DROP  Procedure  AuditActionUpdate
END
GO

PRINT 'Creating Procedure AuditActionUpdate'
GO

/******************************************************************************
**		File: 
**		Name: AuditActionUpdate
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

CREATE Procedure dbo.AuditActionUpdate
(
		@AuditActionId			INT		 				
	,	@Name					VARCHAR(50)					
	,	@Description			VARCHAR(50)				
	,	@SortOrder				INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'AuditAction'		
	
)
AS
BEGIN

	UPDATE	dbo.AuditAction 
	SET		Name			=	@Name				
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	AuditActionId	=	@AuditActionId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AuditActionId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO