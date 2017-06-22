IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipTypeUpdate'
	DROP  Procedure  SystemForeignRelationshipTypeUpdate
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipTypeUpdate
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

CREATE Procedure dbo.SystemForeignRelationshipTypeUpdate
(
		@SystemForeignRelationshipTypeId		INT 			
	,	@Name									VARCHAR(50)				
	,	@Description							VARCHAR (500)			
	,	@SortOrder								INT					
	,	@AuditId								INT					
	,	@TraceId								INT				= NULL
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'SystemForeignRelationshipType'
)
AS
BEGIN
 
 	UPDATE	dbo.SystemForeignRelationshipType 
	SET		Name			=	@Name				
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder						
	WHERE	SystemForeignRelationshipTypeId		=	@SystemForeignRelationshipTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationshipType'
		,	@EntityKey				= @SystemForeignRelationshipTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
		,	@TraceId				= @TraceId
 END		
 GO