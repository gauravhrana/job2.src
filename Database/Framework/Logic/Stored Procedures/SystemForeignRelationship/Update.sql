IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipUpdate')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipUpdate'
	DROP  Procedure  SystemForeignRelationshipUpdate
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipUpdate
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

CREATE Procedure dbo.SystemForeignRelationshipUpdate
(
		@SystemForeignRelationshipId			INT				= NULL	 			
	,	@PrimaryDatabaseId						INT				
	,	@PrimaryEntityId						INT
	,	@ForeignDatabaseId						INT				
	,	@ForeignEntityId						INT					
	,	@FieldName								VARCHAR(50)
	,	@SystemForeignRelationshipTypeId		INT
	,	@AuditId								INT					
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'SystemForeignRelationship'
)
AS
BEGIN
	UPDATE	dbo.SystemForeignRelationship 
	SET		PrimaryDatabaseId				=	@PrimaryDatabaseId				
		,	PrimaryEntityId					=	@PrimaryEntityId
		,	ForeignDatabaseId				=	@ForeignDatabaseId
		,	ForeignEntityId					=	@ForeignEntityId
		,	FieldName						=	@FieldName
		,	SystemForeignRelationshipTypeId	=	@SystemForeignRelationshipTypeId							
	WHERE	SystemForeignRelationshipId	=	@SystemForeignRelationshipId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationship'
		,	@EntityKey				= @SystemForeignRelationshipId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO