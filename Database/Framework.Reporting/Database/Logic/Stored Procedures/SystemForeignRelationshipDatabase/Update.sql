IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDatabaseUpdate')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDatabaseUpdate'
	DROP  Procedure  SystemForeignRelationshipDatabaseUpdate
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDatabaseUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipDatabaseUpdate
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

CREATE Procedure dbo.SystemForeignRelationshipDatabaseUpdate
(
		@SystemForeignRelationshipDatabaseId		INT 			
	,	@Name										VARCHAR(50)				
	,	@Description								VARCHAR (500)			
	,	@SortOrder									INT					
	,	@AuditId									INT					
	,	@TraceId									INT				= NULL
	,	@AuditDate									DATETIME		= NULL	
	,	@SystemEntityType							VARCHAR(50)		= 'SystemForeignRelationshipDatabase'
)
AS
BEGIN
 
 	UPDATE	dbo.SystemForeignRelationshipDatabase 
	SET		Name			=	@Name				
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder						
	WHERE	SystemForeignRelationshipDatabaseId		=	@SystemForeignRelationshipDatabaseId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipDatabase'
		,	@EntityKey				= @SystemForeignRelationshipDatabaseId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
		,	@TraceId				= @TraceId
 END		
 GO